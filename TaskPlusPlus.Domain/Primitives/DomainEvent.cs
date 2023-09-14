using MediatR;

namespace TaskPlusPlus.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
