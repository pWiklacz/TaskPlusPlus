using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Task;

public sealed class TaskNotes : ValueObject
{
    public const int MaxLength = 10000;

    private TaskNotes(string value)
    {
        this.Value = value;
    }

    public static Result<TaskNotes> Create(string taskNotes)
    {
        if (taskNotes.Length > MaxLength)
        {
            return Result.Fail<TaskNotes>(
                new NotesTooLongError(MaxLength));   
        }

        return new TaskNotes(taskNotes);
    }

    public static implicit operator string(TaskNotes taskNotes)
        => taskNotes.Value;

    public static implicit operator TaskNotes(string taskNotes)
        => Create(taskNotes).Value;


    public string Value { get;}
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}