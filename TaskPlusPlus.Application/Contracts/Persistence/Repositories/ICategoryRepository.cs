using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Contracts.Persistence.Repositories;
public interface ICategoryRepository : IGenericRepository<Category, CategoryId>
{
    Task<IReadOnlyList<Category>> GetAllByUserIdAsync(string userId);
}

