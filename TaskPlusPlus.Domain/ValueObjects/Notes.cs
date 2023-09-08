using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class Notes : ValueObject
{
    public const uint MaxLength = 10000;
    public string Value { get; }
    private Notes(string value)
    {
        Value = value;
    }

    public static Result<Notes> Create(string taskNotes)
    {
        if (taskNotes.Length > MaxLength)
        {
            return Result.Fail<Notes>(
                new StringTooLongError(MaxLength, nameof(Notes)));
        }

        return new Notes(taskNotes);
    }
  
    public static implicit operator string(Notes taskNotes)
        => taskNotes.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}