using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Models.Identity;

namespace TaskPlusPlus.API.Controllers;

public class TokenController : BaseController
{
    private readonly IAuthService _authenticationService;

    public TokenController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> Refresh(TokenApiModel tokenApiModel)
    {
        return FromResult(await _authenticationService.RefreshToken(tokenApiModel));
    }


    [Authorize]
    [HttpPost("revoke")]
    public async Task<ActionResult> Revoke(string userId)
    {
        return FromResult(await _authenticationService.RevokeToken(userId));
    }
}
