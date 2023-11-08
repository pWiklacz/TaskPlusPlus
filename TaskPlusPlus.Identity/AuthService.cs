using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using TaskPlusPlus.Application.Constants;
using TaskPlusPlus.Application.Contracts.Identity;
using TaskPlusPlus.Application.Contracts.Infrastructure;
using TaskPlusPlus.Application.Models.Identity;
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
    private readonly IWebHostEnvironment _environment;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender, 
        IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _environment = environment;
    }

    public async Task<Result<AuthResponse>> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Fail(new LoginError());
        var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
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
            EmailConfirmed = true
        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
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

        string templateFolderPath = Path.Combine(_environment.ContentRootPath, "..", "TaskPlusPlus.Infrastructure", "Email", "EmailTemplates");
        string templateFilePath = Path.Combine(templateFolderPath, "ForgotPasswordEmail.html");
        string emailTemplate = await System.IO.File.ReadAllTextAsync(templateFilePath);
        emailTemplate = emailTemplate.Replace("{{BackUrl}}", request.ClientUri);

        var email = new Email
        {
            To = request.Email,
            RecipientName = user.FirstName,
            Subject = "Reset your Password",
            Body = emailTemplate,
            IsHtml = true
        };

        await _emailSender.SendEmailAsync(email);

        return Result.Ok();
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
