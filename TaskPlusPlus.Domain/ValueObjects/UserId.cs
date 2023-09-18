using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class UserId : ValueObject
{
    public const uint MaxLength = 450;
    private UserId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<UserId> Create(string tagName)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return Result.Fail<UserId>(
                new EmptyStringError(nameof(UserId)));
        }

        if (tagName.Length > MaxLength)
        {
            return Result.Fail<UserId>(
                new StringTooLongError(MaxLength, nameof(UserId)));
        }

        return new UserId(tagName);
    }

    public static implicit operator string(UserId name)
        => name.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}