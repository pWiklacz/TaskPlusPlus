namespace TaskPlusPlus.Domain.ValueObjects.Category;

public sealed record CategoryId(ulong Value)
{
    public static implicit operator ulong(CategoryId id)
        => id.Value;

    public static implicit operator CategoryId(ulong id) => new(id);
}