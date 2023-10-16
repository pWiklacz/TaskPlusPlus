using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Models.Identity;

namespace TaskPlusPlus.API.Controllers;

public class AccountController : BaseController
{
    private readonly IAuthService _authenticationService;
    public AccountController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }
}

