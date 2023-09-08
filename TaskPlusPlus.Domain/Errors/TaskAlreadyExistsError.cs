using FluentResults;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Errors;

public sealed class TaskAlreadyExistsError: Error
{
    public TaskAlreadyExistsError(TaskId taskId)
        : base($"Task with id - {taskId} - already exists in this concept.")
    {

    }
}