using FluentResults;
using TaskPlusPlus.Domain.Extensions;

namespace TaskPlusPlus.Domain.Errors;

internal class DateTimeBeforeCurrentTimeError : BaseError
{
    private readonly string _fieldName;

    public DateTimeBeforeCurrentTimeError(string fieldName)
        : base(400, $"{fieldName.SplitCamelCase()} cannot be in the past.")
    {
        _fieldName = fieldName;
    }
}