using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;
public class TwoFactorRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Provider { get; set; }
    [Required]
    public string? Token { get; set; }
}