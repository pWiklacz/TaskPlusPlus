using Microsoft.AspNetCore.Identity;
using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Identity.Errors;

public class ResetPasswordError : BaseError
{
     public ResetPasswordError(List<IdentityError> validationResult)
        : base(400, $"Request failed. Error count: {validationResult!.Count()}")
    {
        foreach (var error in validationResult)
        {
            Metadata.Add(error.Code, error.Description);
        }
    }
}