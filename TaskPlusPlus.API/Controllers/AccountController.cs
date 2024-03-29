﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Models.Identity;
using TaskPlusPlus.Application.Models.Identity.ExternalLogin;

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

    [Authorize]
    [HttpPut("updateSettings")]
    public async Task<IActionResult> UpdateSettings([FromBody] UpdateUserSettingsRequest request)
    {
        return FromResult(await _authenticationService.UpdateUserSettings(request));
    }

    [Authorize]
    [HttpPut("updateEmail")]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest request)
    {
        return FromResult(await _authenticationService.UpdateUserEmail(request));
    }


    [Authorize]
    [HttpPut("changeTwoFactorEnabledStatus")]
    public async Task<IActionResult> ChangeTwoFactorEnabledStatus([FromBody] TwoFactorEnabledStatusRequest request)
    {
        return FromResult(await _authenticationService.ChangeTwoFactorEnabledStatus(request));
    }

    [Authorize]
    [HttpPut("updateData")]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataRequest request)
    {
        return FromResult(await _authenticationService.UpdateUserData(request));
    }

    [Authorize]
    [HttpPut("updatePassword")]
    public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest request)
    {
        return FromResult(await _authenticationService.UpdateUserPassword(request));
    }

    [Authorize]
    [HttpPost("addPassword")]
    public async Task<IActionResult> AddPassword([FromBody] AddPasswordRequest request)
    {
        return FromResult(await _authenticationService.AddUserPassword(request));
    }

    [HttpGet("changeEmailConfirmation")]
    public async Task<IActionResult> ChangeEmailConfirmation([FromQuery] EmailConfirmationRequest request)
    {
        return FromResult(await _authenticationService.ChangeEmailConfirmation(request));
    }


    [HttpPost("twoStepVerification")]
    public async Task<ActionResult<AuthResponse>> TwoStepVerification(TwoFactorRequest request)
    {
        return FromResult(await _authenticationService.TwoStepVerification(request));
    }
}


