using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Persistence.Repositories;
internal sealed class CategoryRepository : GenericRepository<Category, CategoryId>, ICategoryRepository
{
    public CategoryRepository(TaskPlusPlusDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Category>> GetAllByUserIdAsync(string userId)
    {
        return await DbContext.Categories.Where(c => c.UserId == userId)
            .ToListAsync();
    }
}
