using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class CreatingProblemError : BaseError
{
    public CreatingProblemError(string entityName)
        : base(400, $"Problem creating {entityName}")
    {
    }
}