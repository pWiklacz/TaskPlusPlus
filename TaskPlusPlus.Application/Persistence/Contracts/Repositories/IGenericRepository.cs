using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Persistence.Contracts.Repositories;

public interface IGenericRepository<T, TEntityId>
    where T : Entity<TEntityId>
    where TEntityId : class
{
    Task<T> GetByIdAsync(TEntityId id);
    Task<IReadOnlyList<T>> GetAll();
    Task<bool> ExistsByIdAsync(TEntityId id);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}