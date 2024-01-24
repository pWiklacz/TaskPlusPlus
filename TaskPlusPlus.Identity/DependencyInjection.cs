using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Models.Identity;
using TaskPlusPlus.Application.Models.Identity.ExternalLogin;
using TaskPlusPlus.Identity.Models;

namespace TaskPlusPlus.Identity;
public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.Configure<FacebookSettings>(configuration.GetSection("SocialLogin:Facebook"));
        services.Configure<GoogleSettings>(configuration.GetSection("SocialLogin:Google"));

        services.AddDbContext<TaskPlusPlusIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TaskPlusPlusConnectionString"),
                b =>
                {
                    b.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    b.MigrationsAssembly(typeof(TaskPlusPlusIdentityDbContext).Assembly.FullName);
                    b.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "identity");
                }));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TaskPlusPlusIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));

        services.AddTransient<IAuthService, AuthService>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });

        return services;
    }
}
