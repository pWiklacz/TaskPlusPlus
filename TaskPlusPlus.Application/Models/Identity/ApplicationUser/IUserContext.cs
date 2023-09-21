using FluentResults;

namespace TaskPlusPlus.Application.Models.Identity.ApplicationUser;

public interface IUserContext
{
    Result<CurrentUser> GetCurrentUser();
}