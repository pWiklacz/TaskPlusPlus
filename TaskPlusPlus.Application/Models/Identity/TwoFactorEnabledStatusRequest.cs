namespace TaskPlusPlus.Application.Models.Identity;
public class TwoFactorEnabledStatusRequest
{
    public string UserId { get; set; } = null!;
    public bool TwoFactorEnabled { get; set; }
}
