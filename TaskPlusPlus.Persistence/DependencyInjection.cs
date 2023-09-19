using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
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
                configuration.GetConnectionString("TaskPlusPlusConnectionString"))
                .AddInterceptors(auditInterceptor);
        });

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}