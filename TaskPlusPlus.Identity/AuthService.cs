using FluentResults;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using TaskPlusPlus.Application.Constants;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Contracts.Infrastructure;
using TaskPlusPlus.Application.Models.Identity;
using TaskPlusPlus.Application.Models.Identity.ExternalLogin;
using TaskPlusPlus.Application.Models.Mail;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Identity.Errors;
using TaskPlusPlus.Identity.Extensions;
using TaskPlusPlus.Identity.Models;

namespace TaskPlusPlus.Identity;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly JwtSettings _jwtSettings;
    private readonly FacebookSettings _facebookSettings;
    private readonly GoogleSettings _googleSettings;
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        IOptions<FacebookSettings> facebookSettings,
        IOptions<GoogleSettings> googleSettings,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        IWebHostEnvironment environment,
        IHttpClientFactory httpClientFactory)
    {
        _userManager = userManager;
        _httpClientFactory = httpClientFactory;
        _jwtSettings = jwtSettings.Value;
        _googleSettings = googleSettings.Value;
        _facebookSettings = facebookSettings.Value;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _environment = environment;
    }

    public async Task<Result<AuthResponse>> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Fail(new LoginError());

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return Result.Fail(new EmailNotConfirmedError());
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Fail(new LoginError());
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email!,
            UserName = user.UserName!
        };

        return response;
    }

    public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            EmailConfirmed = false
        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var param = new Dictionary<string, string?>
                {
                    {"token", token },
                    {"email", user.Email }
                };

                var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

                string templateFolderPath = Path.Combine(_environment.ContentRootPath, "..", "TaskPlusPlus.Infrastructure", "Email", "EmailTemplates");
                string templateFilePath = Path.Combine(templateFolderPath, "ConfirmationEmail.html");
                string emailTemplate = await System.IO.File.ReadAllTextAsync(templateFilePath);
                emailTemplate = emailTemplate.Replace("{{BackUrl}}", callback);
                emailTemplate = emailTemplate.Replace("{{UserName}}", user.FirstName);
                emailTemplate = emailTemplate.Replace("{{HomeUrl}}", "https://localhost:4200/");

                var email = new Email
                {
                    To = request.Email,
                    RecipientName = user.FirstName,
                    Subject = "Confirm your email",
                    Body = emailTemplate,
                    IsHtml = true
                };

                await _emailSender.SendEmailAsync(email);

                await _userManager.AddToRoleAsync(user, "User");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                return Result.Fail(new RegistrationError(result.Errors.ToList()));
            }
        }
        else
        {
            return Result.Fail(new EmailAlreadyExistError());
        }
    }

    public async Task<Result> ForgotPassword(ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Request"));
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var param = new Dictionary<string, string?>
        {
            {"token", token },
            {"email", request.Email }
        };

        var callback = QueryHelpers.AddQueryString(request.ClientUri, param);

        string templateFolderPath = Path.Combine(_environment.ContentRootPath, "..", "TaskPlusPlus.Infrastructure", "Email", "EmailTemplates");
        string templateFilePath = Path.Combine(templateFolderPath, "ForgotPasswordEmail.html");
        string emailTemplate = await System.IO.File.ReadAllTextAsync(templateFilePath);
        emailTemplate = emailTemplate.Replace("{{BackUrl}}", callback);

        var email = new Email
        {
            To = request.Email,
            RecipientName = user.FirstName,
            Subject = "Reset your Password",
            Body = emailTemplate,
            IsHtml = true
        };

        await _emailSender.SendEmailAsync(email);

        return Result.Ok().WithSuccess("The link has been sent, please check your email to reset your password. \n" +
        "Didn’t get them? Check the email address or ask to resend the instructions.");
    }

    public async Task<Result> ResetPassword(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Request"));
        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (!resetPassResult.Succeeded)
        {
            return Result.Fail(new ResetPasswordError(resetPassResult.Errors.ToList()));
        }

        return Result.Ok().WithSuccess("The password has been successfully reset");
    }

    public async Task<Result> EmailConfirmation(EmailConfirmationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Email Confirmation Request"));
        }

        var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!confirmResult.Succeeded)
        {
            return Result.Fail(new BaseError(400, "Invalid Email Confirmation Request"));
        }

        return Result.Ok().WithSuccess("Email confirmed successfully");
    }

    public async Task<Result<AuthResponse>> ExternalLogin(ExternalAuthRequest request)
    {
        var socialTokenValidateResult = await ValidateSocialToken(request);

        if (socialTokenValidateResult.IsFailed)
            return socialTokenValidateResult.ToResult();

        var info = new UserLoginInfo(request.Provider, socialTokenValidateResult.Value, request.Provider);

        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                var registerSocialUserResult = await RegisterSocialUser(request);

                if (registerSocialUserResult.IsFailed)
                    return registerSocialUserResult.ToResult();

                user = registerSocialUserResult.Value;

                await _userManager.AddLoginAsync(user, info);
            }
            else
            {
                await _userManager.AddLoginAsync(user, info);
            }
        }

        if (user == null)
            return Result.Fail(new BaseError(400, "Invalid External Authentication."));

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email!,
            UserName = user.UserName!
        };

        return response;
    }

    private async Task<Result<string>> ValidateSocialToken(ExternalAuthRequest request)
    {
        return request.Provider switch
        {
            Constants.LoginProviders.Facebook => await ValidateFacebookToken(request),
            Constants.LoginProviders.Google => await ValidateGoogleToken(request),
            _ => Result.Fail(new BaseError(400, $"{request.Provider} provider is not supported."))
        };
    }

    private async Task<Result<string>> ValidateGoogleToken(ExternalAuthRequest externalAuth)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleSettings.TokenAudience! }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.AccessToken, settings);

            return payload.Subject;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private async Task<Result<string>> ValidateFacebookToken(ExternalAuthRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var appAccessTokenResponse = await httpClient.GetFromJsonAsync<FacebookAppAccessTokenResponse>(
            $"https://graph.facebook.com/oauth/access_token?client_id={_facebookSettings.ClientId!}&client_secret={_facebookSettings.ClientSecret!}&grant_type=client_credentials");
        var response =
            await httpClient.GetFromJsonAsync<FacebookTokenValidationResult>(
                $"https://graph.facebook.com/debug_token?input_token={request.AccessToken}&access_token={appAccessTokenResponse!.AccessToken}");

        if (response is null || !response.Data.IsValid)
        {
            return Result.Fail(new BaseError(400, $"{request.Provider} access token is not valid."));
        }

        return response.Data.UserId;
    }

    private async Task<Result<ApplicationUser>> RegisterSocialUser(ExternalAuthRequest request)
    {
        var user = new ApplicationUser()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            UserName = request.Email,
            LastName = request.LastName ?? String.Empty,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            return Result.Fail(new RegistrationError(result.Errors.ToList()));

        await _userManager.AddToRoleAsync(user, "User");

        return user;
    }
    
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimTypes.Uid, user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}
