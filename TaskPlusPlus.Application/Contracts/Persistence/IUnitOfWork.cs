using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity, TEntityId> Repository<TEntity, TEntityId>() 
        where TEntity : Entity<TEntityId>
        where TEntityId : struct;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}