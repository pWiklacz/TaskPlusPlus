using FluentResults;
using TaskPlusPlus.Application.Models.Identity;

namespace TaskPlusPlus.Application.Contracts.Identity;
public interface IAuthService
{
    // Task<AuthResponse> GetCurrentUser(AuthRequest request);
    Task<Result<AuthResponse>> Login(AuthRequest request);
    Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
}
