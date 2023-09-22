namespace TaskPlusPlus.Domain.ValueObjects.Category;

public record struct CategoryId(ulong Value)
{
    public static implicit operator ulong(CategoryId id)
        => id.Value;

    public static implicit operator CategoryId(ulong id) => new(id);
}