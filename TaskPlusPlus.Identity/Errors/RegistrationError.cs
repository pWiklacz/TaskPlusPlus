using Microsoft.AspNetCore.Identity;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Identity.Errors;
internal class RegistrationError : BaseError
{
    public RegistrationError(List<IdentityError> validationResult)
        : base(400, $"Registration failed. Error count: {validationResult!.Count()}")
    {
        foreach (var error in validationResult)
        {
            Metadata.Add(error.Code, error.Description);
        }
    }
}




