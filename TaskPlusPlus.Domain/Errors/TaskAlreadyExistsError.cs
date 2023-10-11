using FluentResults;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Errors;

public sealed class TaskAlreadyExistsError : BaseError
{
    public TaskAlreadyExistsError(TaskId taskId)
        : base(409, $"Task with id - {taskId} - already exists in this concept.")
    {

    }
}