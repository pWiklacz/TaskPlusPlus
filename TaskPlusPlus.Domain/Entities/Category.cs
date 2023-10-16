using System.Text.Json.Serialization;
using FluentResults;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Domain.Entities;

public class Category : Entity<CategoryId>
{
    public CategoryName Name { get; private set; } = null!;
    public bool IsImmutable { get; private set; }
    public bool IsFavorite { get; private set; }
    public ColorHex ColorHex { get; private set; } = null!;
    public UserId UserId { get; private set; } = null!;

    public const string SystemOwner = "system";

    private Category() { }

    private Category(
        CategoryName name,
        bool isImmutable,
        ColorHex colorHex,
        UserId userId,
        bool isFavorite = false
        )
    {
        Name = name;
        IsImmutable = isImmutable;
        IsFavorite = isFavorite;
        ColorHex = colorHex;
        UserId = userId;
    }

    public static Result<Category> Create(
        string name,
        bool isImmutable,
        bool isFavorite,
        string colorHex,
        string userId)
    {
        var errors = new List<IError>();

        var nameResult = CategoryName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        var colorResult = ColorHex.Create(colorHex);
        if (colorResult.IsFailed)
            errors.AddRange(colorResult.Errors);

        var userIdResult = UserId.Create(userId);
        if (userIdResult.IsFailed)
            errors.AddRange(userIdResult.Errors);

        if (errors.Any())
            return Result.Fail<Category>(errors);

        var category = new Category(
            nameResult.Value,
            isImmutable,
            colorResult.Value,
            userIdResult.Value,
            isFavorite
            );

        return category;
    }

    public void ChangeFavoriteState(bool favorite)
        => IsFavorite = favorite;

    public Result UpdateName(string name)
    {
        var nameResult = CategoryName.Create(name);
        if (nameResult.IsFailed)
            return Result.Fail(nameResult.Errors);
        Name = nameResult.Value;
        return Result.Ok();
    }
    public Result ChangeColor(string color)
    {
        var colorResult = ColorHex.Create(color);
        if (colorResult.IsFailed)
            return Result.Fail(colorResult.Errors);
        ColorHex = colorResult.Value;
        return Result.Ok();
    }
}