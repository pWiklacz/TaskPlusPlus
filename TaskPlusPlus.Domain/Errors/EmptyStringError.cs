using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

public class EmptyStringError : Error
{
    public EmptyStringError(string fieldName)
    : base($"{fieldName.SplitCamelCase()} cannot be empty.")
    {
        
    }
}