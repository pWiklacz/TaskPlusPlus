using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;
public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required]
    public string ClientUri { get; set; } = null!;
}
