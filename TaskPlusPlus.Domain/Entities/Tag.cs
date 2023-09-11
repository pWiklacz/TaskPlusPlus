using FluentResults;
using System.Xml.Linq;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPlusPlus.Domain.Entities;

public sealed class Tag : Entity
{
    public override TagId Id { get; }
    internal TagName Name { get; private set; }
    private ColorHex _colorHex;
    private bool _isFavorite;
    private Tag(TagName name, 
        ColorHex colorHex, 
        bool isFavorite)
    {
        Name = name;
        _colorHex = colorHex;
        Id = new TagId(new Guid());
        _isFavorite = isFavorite;
    }

    public Result<Tag> Create(
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
        return tag;
    }

    public void ChangeFavoriteState()
        => _isFavorite = !_isFavorite;
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
        _colorHex = colorResult.Value;
        return Result.Ok();
    }
}