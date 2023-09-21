using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;

namespace TaskPlusPlus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddScoped<IUserContext, UserContext>();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}