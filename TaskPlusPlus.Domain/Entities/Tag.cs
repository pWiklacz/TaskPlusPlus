using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Tag : Entity
{
    public override TagId Id { get; }
    public TagName Name { get; set; }
    public ColorHex ColorHex { get; set; }
    public bool IsFavorite { get; set; }
    public Tag(TagName name, ColorHex colorHex, bool isFavorite = false)
    {
        Name = name;
        ColorHex = colorHex;
        Id = new TagId(new Guid());
        IsFavorite = isFavorite;
    }
}