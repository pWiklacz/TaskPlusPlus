using FluentResults;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class AuthenticationRequiredError : BaseError
{
    public AuthenticationRequiredError()
        : base(401, "Authentication is required to access this resource")
    {
    }
}
