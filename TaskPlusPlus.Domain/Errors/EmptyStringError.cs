using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

public class EmptyStringError : BaseError
{
    public EmptyStringError(string fieldName)
    : base(400, $"{fieldName.SplitCamelCase()} cannot be empty.")
    {

    }
}