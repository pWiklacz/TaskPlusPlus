using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Project;

public sealed record ProjectId(Guid Value) : TypedId(Value);