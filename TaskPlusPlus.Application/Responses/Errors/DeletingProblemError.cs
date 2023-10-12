using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class DeletingProblemError : BaseError
{
    public DeletingProblemError(string entityName)
        : base(400, $"Problem deleting {entityName}")
    {
    }
}