using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Category;

public sealed class CategoryName : ValueObject
{
    public const ushort MaxLength = 50;
    private CategoryName(string value)
    {
        Value = value; 
    }

    public string Value { get; }

    public static Result<CategoryName> Create(string projectName)
    {
        if (string.IsNullOrEmpty(projectName))
        {
            return Result.Fail<CategoryName>(
                new EmptyStringError(nameof(CategoryName)));
        }

        if (projectName.Length > MaxLength)
        {
            return Result.Fail<CategoryName>(
                new StringTooLongError(MaxLength, nameof(CategoryName)));
        }

        return new CategoryName(projectName);
    }

    public static implicit operator string(CategoryName name)
        => name.Value;
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}