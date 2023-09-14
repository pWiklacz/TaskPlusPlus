namespace TaskPlusPlus.Domain.Primitives;

public interface IEntity
{
    ICollection<DomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}