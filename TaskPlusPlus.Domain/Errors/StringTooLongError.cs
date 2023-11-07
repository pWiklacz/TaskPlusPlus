using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

public class StringTooLongError : BaseError
{
    public StringTooLongError(uint maxLength, string fieldName)
    : base(400, $"{fieldName.SplitCamelCase()} too long. Maximum length is {maxLength}.")
    {

    }
}