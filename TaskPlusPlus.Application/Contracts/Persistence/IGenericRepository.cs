using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Contracts.Persistence;

public interface IGenericRepository<TEntity, in TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct
{
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<IReadOnlyList<TEntity>> ListAllAsync();
    Task<bool> ExistsByIdAsync(TEntityId id);
    Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity> spec);
    Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
    Task<int> CountAsync(ISpecification<TEntity> spec);
    TEntity Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}