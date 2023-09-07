using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

public class StringTooLongError : Error
{
    public StringTooLongError(uint maxLength, string fieldName)
    : base($"{fieldName.SplitCamelCase()} too long. Maximum length is {maxLength}.")
    {
        
    }
} 