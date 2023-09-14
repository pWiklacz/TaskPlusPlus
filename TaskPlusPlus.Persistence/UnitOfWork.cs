using TaskPlusPlus.Application.Contracts.Persistence;

namespace TaskPlusPlus.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly TaskPlusPlusDbContext _context;

    public UnitOfWork(TaskPlusPlusDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return _context.SaveChangesAsync(cancellationToken);
    }
}