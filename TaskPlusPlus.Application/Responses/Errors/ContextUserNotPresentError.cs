using FluentResults;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class ContextUserNotPresentError : Error
{
    public ContextUserNotPresentError()
        : base("Context user is not present")
    {
    }
}
