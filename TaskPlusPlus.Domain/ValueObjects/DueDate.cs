using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class DueDate : ValueObject
{
    private DueDate(DateOnly value)
    {
        Value = value;
    }

    public DateOnly Value { get; }

    public static Result<DueDate> Create(DateOnly dueDate)
    {
        return new DueDate(dueDate);
    }

    public static implicit operator DateOnly(DueDate dueDate)
        => dueDate.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static bool operator <=(DueDate left, DueDate right)
    {
        return left.Value <= right.Value;
    }

    public static bool operator >=(DueDate left, DueDate right)
    {
        return left.Value >= right.Value;
    }
}