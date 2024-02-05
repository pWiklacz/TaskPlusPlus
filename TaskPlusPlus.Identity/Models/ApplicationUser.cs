using Microsoft.AspNetCore.Identity;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;

namespace TaskPlusPlus.Identity.Models;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserSettings Settings { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

}
