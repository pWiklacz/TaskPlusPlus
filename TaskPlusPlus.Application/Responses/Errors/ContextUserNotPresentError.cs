using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class ContextUserNotPresentError : BaseError
{
    public ContextUserNotPresentError()
        : base(401,"Context user is not present")
    {
    }
}
