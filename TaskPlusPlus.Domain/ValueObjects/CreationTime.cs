using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class CreationTime : ValueObject
{
    private CreationTime(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<CreationTime> Create(DateTime creationTime)
    {
        if (creationTime < DateTime.Now)
        {
            return Result.Fail<CreationTime>(
                new DateTimeBeforeCurrentTimeError(nameof(CreationTime)));
        }

        return new CreationTime(creationTime);
    }

    public static implicit operator DateTime(CreationTime creationTime)
        => creationTime.Value;
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}