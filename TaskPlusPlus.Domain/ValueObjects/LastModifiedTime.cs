using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class LastModifiedTime : ValueObject
{
    private LastModifiedTime(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<LastModifiedTime> Create(DateTime creationTime)
    {
        if (creationTime < DateTime.Now)
        {
            return Result.Fail<LastModifiedTime>(
                new DateTimeBeforeCurrentTimeError(nameof(LastModifiedTime)));
        }

        return new LastModifiedTime(creationTime);
    }

    public static implicit operator DateTime(LastModifiedTime creationTime)
        => creationTime.Value;

    public static implicit operator LastModifiedTime(DateTime creationTime)
        => Create(creationTime).Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}