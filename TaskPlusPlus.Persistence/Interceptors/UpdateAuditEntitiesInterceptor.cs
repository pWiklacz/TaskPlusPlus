using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Persistence.Interceptors;

public sealed class UpdateAuditEntitiesInterceptor
: SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavedChangesAsync(eventData, result, cancellationToken);

        IEnumerable<EntityEntry<IAuditEntity>> entries = dbContext.ChangeTracker
            .Entries<IAuditEntity>();

        foreach (var entityEntry in entries)
        {
            if(entityEntry.State ==  EntityState.Added)
                entityEntry.Property(e => e.CreatedOnUtc).CurrentValue = DateTime.Now;
            if(entityEntry.State == EntityState.Modified)
                entityEntry.Property(e => e.LastModifiedOnUtc).CurrentValue = DateTime.Now;
        }
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}