using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Identity.Errors;
internal class LoginError : BaseError
{
    public LoginError()
        : base(401, "Uh-oh! It seems like we have an extra task for you!. 🤔 " +
                    "Your email and password aren't in sync with our task force. " +
                    "Double-check your login details or consider bribing your computer with a cookie. 🍪😄 ")
    {
    }
}
