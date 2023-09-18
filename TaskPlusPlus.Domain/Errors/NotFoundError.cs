using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string entityName, ulong id)
    : base($"'{entityName}' not found for Id '{id}'")
    {
        Metadata.Add("ErrorCode", $"{entityName}.not.found");
    }
}