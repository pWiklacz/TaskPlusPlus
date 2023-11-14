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
        return FromResult(await _authenticationService.Login(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {

        return FromResult(await _authenticationService.Register(request));
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        return FromResult(await _authenticationService.ForgotPassword(request));
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        return FromResult(await _authenticationService.ResetPassword(request));
    }

    [HttpGet("emailConfirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] EmailConfirmationRequest request)
    {
        return FromResult(await _authenticationService.EmailConfirmation(request));
    }

    [HttpPost("externalLogin")]
    public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthRequest request)
    {
        return FromResult(await _authenticationService.ExternalLogin(request));
    }

}

