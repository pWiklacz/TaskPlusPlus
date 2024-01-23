using FluentResults;
using TaskPlusPlus.Application.Models.Identity;
using TaskPlusPlus.Application.Models.Identity.ExternalLogin;

namespace TaskPlusPlus.Application.Contracts.Identity;
public interface IAuthService
{
    Task<Result<AuthResponse>> Login(AuthRequest request);
    Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
    Task<Result> ForgotPassword(ForgotPasswordRequest request);
    Task<Result> ResetPassword(ResetPasswordRequest request);
    Task<Result> EmailConfirmation(EmailConfirmationRequest request);
    Task<Result<AuthResponse>> ExternalLogin(ExternalAuthRequest request);
    Task<Result> UpdateUserSettings(UpdateUserSettingsRequest request);
    Task<Result<AuthResponse>> UpdateUserData(UpdateUserDataRequest request);
    Task<Result<AuthResponse>> AddUserPassword(AddPasswordRequest request);
    Task<Result> ChangeEmailConfirmation(EmailConfirmationRequest request);
    Task<Result> UpdateUserEmail(UpdateEmailRequest request);
    Task<Result> UpdateUserPassword(ChangePasswordRequest request);
}
