using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class Notes : ValueObject
{
    public const uint MaxLength = 10000;

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

    public static implicit operator Notes(string taskNotes)
        => Create(taskNotes).Value;


    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}