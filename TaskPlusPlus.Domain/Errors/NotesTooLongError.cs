using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class NotesTooLongError : Error
{
    public NotesTooLongError(int maxLength)
    : base($"Notes are too long. Maximum length is {maxLength}.")
    {
        
    }
}