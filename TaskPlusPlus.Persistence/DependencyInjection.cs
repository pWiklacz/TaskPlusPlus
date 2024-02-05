using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Persistence.Interceptors;
using TaskPlusPlus.Persistence.Repositories;

namespace TaskPlusPlus.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditEntitiesInterceptor>();
        services.AddDbContext<TaskPlusPlusDbContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetService<UpdateAuditEntitiesInterceptor>()!;

            options.UseSqlServer(
                configuration.GetConnectionString("TaskPlusPlusConnectionString"),
                o =>
                {
                    o.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "application");
                }
                )
                .AddInterceptors(auditInterceptor);
        });

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
} 