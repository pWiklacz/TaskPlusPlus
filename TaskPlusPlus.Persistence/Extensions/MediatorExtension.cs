using MediatR;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Persistence.Extensions;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, TaskPlusPlusDbContext context)
    {
        var domainEvents = context.ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents();
                e.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}