using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

internal class DateTimeBeforeCurrentTimeError : Error
{
    private readonly string _fieldName;

    public DateTimeBeforeCurrentTimeError(string fieldName) 
        : base($"{fieldName.SplitCamelCase()} cannot be in the past.")
    {
        _fieldName = fieldName;
    }
}