using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class DueDate : ValueObject
{
    private DueDate(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<DueDate> Create(DateTime dueDate)
    {
        if (dueDate <= DateTime.Now)
        {
            return Result.Fail<DueDate>(
                new DateTimeBeforeCurrentTimeError(nameof(dueDate)));
        }

        return new DueDate(dueDate);
    }

    public static implicit operator DateTime(DueDate dueDate)
        => dueDate.Value;

    public static implicit operator DueDate(DateTime dueDate)
        => Create(dueDate).Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}