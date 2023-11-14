using Microsoft.OpenApi.Models;
using TaskPlusPlus.Application;
using TaskPlusPlus.Identity;
using TaskPlusPlus.Infrastructure;
using TaskPlusPlus.Persistence;

namespace TaskPlusPlus.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddHttpContextAccessor();
        AddSwaggerDoc(services);
        services
            .AddApplication()
            .AddInfrastructure(config)
            .AddPersistence(config)
            .AddIdentity(config);

        // Add services to the container.
        services.AddHttpClient();
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(opt =>
                     {
                         opt.AddPolicy("CorsPolicy", policy =>
                         {
                             policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                         });
                     });

        return services;
    }

    private static void AddSwaggerDoc(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TaskPlusPlus API",

            });

        });
    }
}
