using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Task;

public sealed record TaskId(Guid Value) : TypedId(Value);
