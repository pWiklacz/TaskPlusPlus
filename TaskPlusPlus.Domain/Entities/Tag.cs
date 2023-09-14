using FluentResults;
using TaskPlusPlus.Domain.DomainEvents;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Domain.Entities;

public sealed class Tag : Entity<TagId>
{
    //public override TagId Id { get; } = null!;
    public TagName Name { get; private set; } = null!;
    public ColorHex ColorHex { get; private set; } = null!;
    public bool IsFavorite { get; private set; }

    private Tag(
        TagName name, 
        ColorHex colorHex, 
        bool isFavorite 
        )
    {
        Name = name;
        ColorHex = colorHex;
        IsFavorite = isFavorite;
    }
    private Tag() { }
    public static Result<Tag> Create(
        string name, 
        string colorHex,
        bool isFavorite = false)
    {
        var errors = new List<IError>();

        var nameResult = TagName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        var colorResult = ColorHex.Create(colorHex);
        if (colorResult.IsFailed)
            errors.AddRange(colorResult.Errors);

        if (errors.Any())
            return Result.Fail<Tag>(errors);

        var tag = new Tag(
            nameResult.Value,
            colorResult.Value,
            isFavorite);

        tag.RaiseDomainEvent(new TagCreatedDomainEvent(Guid.NewGuid(), tag.Id));

        return tag;
    }

    public void ChangeFavoriteState()
        => IsFavorite = !IsFavorite;
    public Result UpdateName(string name)
    {
        var nameResult = TagName.Create(name);
        if (nameResult.IsFailed)
            return Result.Fail(nameResult.Errors);
        Name = nameResult.Value;
        return Result.Ok();
    }
    public Result ChangeColor(string color)
    {
        var colorResult = ColorHex.Create(color);
        if(colorResult.IsFailed)
            return Result.Fail(colorResult.Errors);
        ColorHex = colorResult.Value;
        return Result.Ok();
    }
}