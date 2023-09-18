using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Task;

public sealed class TaskName : ValueObject
{
    public const ushort MaxLength = 500;
    private TaskName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<TaskName> Create(string taskName)
    {
        if (string.IsNullOrEmpty(taskName))
        {
            
            return Result.Fail<TaskName>(
                new EmptyStringError(nameof(TaskName)));
        }

        if (taskName.Length > MaxLength)
        {
            return Result.Fail<TaskName>(
                new StringTooLongError(MaxLength,nameof(TaskName)));
        }

        return new TaskName(taskName);
    }

    public static implicit operator string(TaskName name)
        => name.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}