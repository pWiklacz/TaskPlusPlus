using TaskPlusPlus.Application.Models.Identity;

namespace TaskPlusPlus.Application.Contracts.Identity;
public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}
