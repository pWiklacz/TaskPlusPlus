using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

internal class DateTimeBeforeCurrentTimeError : Error
{
    private readonly string _fieldName;

    public DateTimeBeforeCurrentTimeError(string fieldName)
        : base($"{fieldName} cannot be in the past.")
    {
        _fieldName = fieldName;
    }
}