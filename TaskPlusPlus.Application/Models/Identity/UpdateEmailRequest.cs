using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;
public class UpdateEmailRequest
{
    public string UserId { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    [Required]
    public string ClientURI { get; set; } = null!;
}
