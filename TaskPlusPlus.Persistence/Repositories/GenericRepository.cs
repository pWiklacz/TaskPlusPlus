using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Persistence.Repositories;

internal abstract class GenericRepository<TEntity, TEntityId> : IGenericRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly TaskPlusPlusDbContext DbContext;

    protected GenericRepository(TaskPlusPlusDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
         return await DbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(TEntityId id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

    public TEntity Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}