using System.Collections;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Persistence.Repositories;

namespace TaskPlusPlus.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TaskPlusPlusDbContext _context;
    private Hashtable _repositories = new();
    public UnitOfWork(TaskPlusPlusDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public IGenericRepository<TEntity, TEntityId> Repository<TEntity, TEntityId>()
        where TEntity : Entity<TEntityId>
        where TEntityId : struct
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
            return (IGenericRepository<TEntity, TEntityId>)_repositories[type]!;

        var repositoryType = typeof(GenericRepository<,>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        _repositories.Add(type, repositoryInstance);

        return (IGenericRepository<TEntity, TEntityId>)_repositories[type]!;
    }
}
