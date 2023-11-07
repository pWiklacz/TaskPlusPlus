using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Identity.Errors;
internal class EmailAlreadyExistError : BaseError
{
    public EmailAlreadyExistError()
        : base(409, "Oops! That email's like a best-selling novel – already taken! 😄📚 " +
                    "Please try using a different address.")
    {
    }
}
