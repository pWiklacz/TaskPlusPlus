namespace TaskPlusPlus.Domain.ValueObjects.Tag;

public record struct TagId(ulong Value)
{
    public static implicit operator ulong(TagId id)
        => id.Value;

    public static implicit operator TagId(ulong id) => new(id);
}