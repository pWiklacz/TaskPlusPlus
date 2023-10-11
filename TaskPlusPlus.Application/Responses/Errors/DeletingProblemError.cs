using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class DeleteProblemError : BaseError
{
    public DeleteProblemError(string entityName)
        : base(400, $"Problem deleting {entityName}")
    {
    }
}