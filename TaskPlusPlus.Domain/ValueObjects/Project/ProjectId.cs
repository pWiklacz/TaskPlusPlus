namespace TaskPlusPlus.Domain.ValueObjects.Project;

public sealed record ProjectId(ulong Value)
{
    public static implicit operator ulong(ProjectId id)
        => id.Value;

    public static implicit operator ProjectId(ulong id) => new(id);
}