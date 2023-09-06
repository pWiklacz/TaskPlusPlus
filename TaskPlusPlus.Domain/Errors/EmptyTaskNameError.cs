using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class EmptyTaskNameError : Error
{
    public EmptyTaskNameError()
    : base("Task name cannot be empty.")
    {
        
    }
}