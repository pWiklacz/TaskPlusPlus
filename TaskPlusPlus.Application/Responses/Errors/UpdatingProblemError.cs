using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class UpdatingProblemError : BaseError
{
    public UpdatingProblemError(string entityName)
        : base(400, $"Problem updating {entityName}")
    {
    }
}