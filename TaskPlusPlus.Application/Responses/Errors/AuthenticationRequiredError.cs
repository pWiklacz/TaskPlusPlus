using FluentResults;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class AuthenticationRequiredError : Error
{
    public AuthenticationRequiredError()
        : base("Authentication is required to access this resource")
    {
    }
}
