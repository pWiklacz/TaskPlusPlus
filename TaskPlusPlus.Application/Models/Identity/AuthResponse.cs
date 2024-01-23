using TaskPlusPlus.Application.Models.Identity.ApplicationUser;

namespace TaskPlusPlus.Application.Models.Identity;
public class AuthResponse
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public UserSettings Settings { get; set; } = null!;
    public bool HasPassword { get; set; }
}
