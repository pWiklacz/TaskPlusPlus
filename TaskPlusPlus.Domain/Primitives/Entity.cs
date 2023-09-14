using System.Runtime.ExceptionServices;

namespace TaskPlusPlus.Domain.Primitives;

public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>>, IEntity
    where TEntityId : class
{
    public TEntityId Id { get; } = null!;
    private readonly List<DomainEvent> _domainEvents = new();
    public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents()
    => _domainEvents.Clear();
    public static bool operator ==(Entity<TEntityId>? left, Entity<TEntityId>? right)
        => left is not null && right is not null && left.Equals(right);
    public static bool operator !=(Entity<TEntityId>? left, Entity<TEntityId>? right)
        => !(left == right);
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not Entity<TEntityId> entity)
            return false;

        return entity.Id == Id;

    }
    public bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;

    }
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}