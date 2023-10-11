using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class NotFoundError : BaseError
{
    public NotFoundError(string entityName, ulong id)
    : base(404,$"'{entityName}' not found for Id '{id}'")
    {
        Metadata.Add("ErrorCode", $"{entityName}.not.found");
    }
}