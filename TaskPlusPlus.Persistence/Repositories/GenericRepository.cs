using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Persistence.Specifications;

namespace TaskPlusPlus.Persistence.Repositories;

internal class GenericRepository<TEntity, TEntityId> : IGenericRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct
{
    private readonly TaskPlusPlusDbContext _dbContext;

    public GenericRepository(TaskPlusPlusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return await _dbContext.Set<TEntity>()
            .SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<IReadOnlyList<TEntity>> ListAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(TEntityId id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

    public async Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public TEntity Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity, TEntityId>
            .GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
    }

}