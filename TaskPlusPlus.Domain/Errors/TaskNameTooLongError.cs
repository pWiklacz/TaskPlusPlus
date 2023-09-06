using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class TaskNameTooLongError : Error
{
    public TaskNameTooLongError(int maxLength)
    : base($"Task name is too long. Maximum length is {maxLength}.")
    {
        
    }
}