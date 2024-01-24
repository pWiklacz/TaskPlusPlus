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
using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Application.Constants;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Contracts.Infrastructure;
using TaskPlusPlus.Application.Models.Identity;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Models.Identity.ExternalLogin;
using TaskPlusPlus.Application.Models.Mail;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Identity.Errors;
using TaskPlusPlus.Identity.Models;
using Microsoft.Extensions.Hosting;

namespace TaskPlusPlus.Identity;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TaskPlusPlusIdentityDbContext _dbContext;
    private readonly IEmailSender _emailSender;
    private readonly JwtSettings _jwtSettings;
    private readonly FacebookSettings _facebookSettings;
    private readonly GoogleSettings _googleSettings;
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IWebHostEnvironment _env;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        IOptions<FacebookSettings> facebookSettings,
        IOptions<GoogleSettings> googleSettings,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender,
        IWebHostEnvironment environment,
        IHttpClientFactory httpClientFactory, 
        TaskPlusPlusIdentityDbContext dbContext, 
        IWebHostEnvironment env)
    {
        _userManager = userManager;
        _httpClientFactory = httpClientFactory;
        _dbContext = dbContext;
        _env = env;
        _jwtSettings = jwtSettings.Value;
        _googleSettings = googleSettings.Value;
        _facebookSettings = facebookSettings.Value;
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
            UserName = user.UserName!,
            Settings = user.Settings,
            HasPassword = true
        };

        return response;
    }

    public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var settings = new UserSettings();
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            EmailConfirmed = false,
            Settings = settings
        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail != null) return Result.Fail(new EmailAlreadyExistError());
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) return Result.Fail(new RegistrationError(result.Errors.ToList()));
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

        if (_env.IsDevelopment())
        {
            emailTemplate = emailTemplate.Replace("{{HomeUrl}}", "https://localhost:4200/");
        }
        else if (_env.IsProduction())
        {
            emailTemplate = emailTemplate.Replace("{{HomeUrl}}", "https://taskplusplus.azurewebsites.net/");
        }

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
        return Result.Ok(new RegistrationResponse() { UserId = user.Id }).WithSuccess(
            "Your account has been successfully created." +
            " We sent a confirmation link to the email address you provided. Please check your inbox and confirm the email before logging in.");
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

        var userHasPassword = await _userManager.HasPasswordAsync(user);

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email!,
            UserName = user.UserName!,
            Settings = user.Settings,
            HasPassword = userHasPassword
        };

        return response;
    }

    public async Task<Result> UpdateUserSettings(UpdateUserSettingsRequest request)
    {
        var user = await _dbContext.Set<ApplicationUser>().SingleOrDefaultAsync(u => u.Id == request.UserId);
        
        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Setting Update Request"));
        }

        user.Settings = request.Settings;

        _dbContext.Set<ApplicationUser>().Update(user);
        var saveResult = await _dbContext.SaveChangesAsync();


        if (saveResult <= 0)
        {
            return Result.Fail(new BaseError(400, "Invalid Setting Update Request"));
        }

        return Result.Ok().WithSuccess("Settings updated successfully");
    }

    public async Task<Result> UpdateUserPassword(ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Password Update Request"));
        }

        var updatePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!updatePasswordResult.Succeeded)
        {
            if (updatePasswordResult.Errors.Any(error => error.Code == "PasswordMismatch"))
            {
                return Result.Fail(new BaseError(400, "You entered an incorrect password. Please confirm and try again."));
            }

            return Result.Fail(new BaseError(400, "Invalid Password Update Request"));
        }

        return Result.Ok().WithSuccess("Password changed successfully");
    }

    public async Task<Result> UpdateUserEmail(UpdateEmailRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Email Update Request"));
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Fail(new LoginError());
        }

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);

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
        emailTemplate = emailTemplate.Replace("{{NewEmail}}", request.Email);

        if (_env.IsDevelopment())
        {
            emailTemplate = emailTemplate.Replace("{{HomeUrl}}", "https://localhost:4200/");
        }
        else if (_env.IsProduction())
        {
            emailTemplate = emailTemplate.Replace("{{HomeUrl}}", "https://taskplusplus.azurewebsites.net/");
        }
        
        var email = new Email
        {
            To = request.Email,
            RecipientName = user.FirstName,
            Subject = "Confirmation of Email Address Change",
            Body = emailTemplate,
            IsHtml = true
        };

        await _emailSender.SendEmailAsync(email);
         
        return Result.Ok();
    }

    public async Task<Result> ChangeEmailConfirmation(EmailConfirmationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Email Confirmation Request"));
        }

        var confirmResult = await _userManager.ChangeEmailAsync(user, request.Email, request.Token);


        if (!confirmResult.Succeeded)
        {
            return Result.Fail(new BaseError(400, "Invalid Email Confirmation Request"));
        }

        return Result.Ok().WithSuccess("Email changed successfully");
    }

    public async Task<Result<AuthResponse>> AddUserPassword(AddPasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid Add Password Request"));
        }

        var userHasPassword = await _userManager.HasPasswordAsync(user);

        if (userHasPassword)
        {
            return Result.Fail(new BaseError(400, "Invalid Add Password Request"));
        }

        var result = await _userManager.AddPasswordAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result.Fail(new BaseError(400, "Invalid Add Password Request"));
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email!,
            UserName = user.UserName!,
            Settings = user.Settings,
            HasPassword = true
        };

        return Result.Ok(response).WithSuccess("Password added successfully");
    }

    public async Task<Result<AuthResponse>> UpdateUserData(UpdateUserDataRequest request)
    {
        var user = await _dbContext.Set<ApplicationUser>().SingleOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
        {
            return Result.Fail(new BaseError(400, "Invalid User Data Update Request"));
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        _dbContext.Set<ApplicationUser>().Update(user);
        var saveResult = await _dbContext.SaveChangesAsync();


        if (saveResult <= 0)
        {
            return Result.Fail(new BaseError(400, "Invalid User Data Update Request"));
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email!,
            UserName = user.UserName!,
            Settings = user.Settings,
            HasPassword = true
        };

        return Result.Ok(response).WithSuccess("Profile updated successfully");
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
        var settings = new UserSettings();
        var user = new ApplicationUser()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            UserName = request.Email,
            LastName = request.LastName ?? String.Empty,
            SecurityStamp = Guid.NewGuid().ToString(),
            Settings = settings
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
        var userHasPassword = await _userManager.HasPasswordAsync(user);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimTypes.Uid, user.Id),
            new Claim(CustomClaimTypes.HasPassword, userHasPassword.ToString())
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
