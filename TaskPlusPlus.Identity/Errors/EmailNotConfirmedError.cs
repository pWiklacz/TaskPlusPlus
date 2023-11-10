using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Identity.Errors
{
    public class EmailNotConfirmedError : BaseError
    {
    public EmailNotConfirmedError()
     : base(401, "Email is not confirmed")
        {
        }
    }
}