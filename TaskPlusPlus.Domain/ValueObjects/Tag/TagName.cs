using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Tag;

public sealed class TagName : ValueObject
{
    public const byte MaxLength = 50;
    private TagName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<TagName> Create(string tagName)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return Result.Fail<TagName>(
                new EmptyStringError(nameof(TagName)));
        }

        if (tagName.Length > MaxLength)
        {
            return Result.Fail<TagName>(
                new StringTooLongError(MaxLength, nameof(TagName)));
        }

        return new TagName(tagName);
    }

    public static implicit operator string(TagName name)
        => name.Value;

    public static implicit operator TagName(string name)
        => Create(name).Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}