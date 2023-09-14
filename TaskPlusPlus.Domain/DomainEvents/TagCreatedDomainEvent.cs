using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Domain.DomainEvents;

public sealed record TagCreatedDomainEvent(Guid Id, TagId TagId) : DomainEvent(Id);
