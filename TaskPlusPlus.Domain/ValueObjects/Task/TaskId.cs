namespace TaskPlusPlus.Domain.ValueObjects.Task;

public record struct TaskId(ulong Value)
{
    public static implicit operator ulong(TaskId id)
        => id.Value;

    public static implicit operator TaskId(ulong id) => new(id);
}
