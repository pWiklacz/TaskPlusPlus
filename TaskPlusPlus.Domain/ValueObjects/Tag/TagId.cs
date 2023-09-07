using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Tag;

public sealed record TagId(Guid Value) : TypedId(Value);