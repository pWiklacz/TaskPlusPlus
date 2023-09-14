using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TaskPlusPlus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}