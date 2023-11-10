namespace TaskPlusPlus.Application.Models.Identity;

public class EmailConfirmationRequest
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}