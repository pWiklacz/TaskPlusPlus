using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Application.Persistence.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> Get(TypedId id);
    Task<IReadOnlyList<T>> GetAll();
    Task<T> Add(T entity);
    Task<bool> Exists(TypedId id);
    Task Update(T entity);
    Task Delete(T entity);
}