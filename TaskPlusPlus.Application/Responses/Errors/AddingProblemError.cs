using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class AddingProblemError : BaseError
{
    public AddingProblemError(string entityName)
        : base(400, $"Problem adding {entityName}")
    {
    }
}