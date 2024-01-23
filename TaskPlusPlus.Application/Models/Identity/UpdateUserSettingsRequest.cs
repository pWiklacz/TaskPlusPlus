using TaskPlusPlus.Application.Models.Identity.ApplicationUser;

namespace TaskPlusPlus.Application.Models.Identity;
public class UpdateUserSettingsRequest
{
    public string UserId { get; set; } = null!;
    public UserSettings Settings { get; set; } = null!;
}
