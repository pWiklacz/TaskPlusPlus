using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Contracts.Persistence.Repositories;

public interface IGenericRepository<T, TEntityId>
    where T : Entity<TEntityId>
    where TEntityId : class
{
    Task<T?> GetByIdAsync(TEntityId id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<bool> ExistsByIdAsync(TEntityId id);
    T Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}