using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Http;
using TaskPlusPlus.Application.Constants;
using TaskPlusPlus.Application.Responses.Errors;

namespace TaskPlusPlus.Application.Models.Identity.ApplicationUser;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Result<CurrentUser> GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            return Result.Fail(new ContextUserNotPresentError());
        }

        if (user.Identity is not { IsAuthenticated: true })
        {
            return Result.Fail(new AuthenticationRequiredError());
        }

        var id = user.FindFirst(c => c.Type == CustomClaimTypes.Uid)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        return new CurrentUser(id, email, roles);
    }
}
