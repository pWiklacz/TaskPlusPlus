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
        return new DueDate(dueDate);
    }

    public static implicit operator DateTime(DueDate dueDate)
        => dueDate.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}