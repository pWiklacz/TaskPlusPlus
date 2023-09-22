using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Contracts.Persistence.Repositories;

public interface IGenericRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct
{
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<bool> ExistsByIdAsync(TEntityId id);
    TEntity Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}