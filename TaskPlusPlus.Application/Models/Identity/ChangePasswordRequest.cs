using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;
public class ChangePasswordRequest
{
    public string UserId { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = null!;


    [Required]
    [MinLength(6)]
    public string CurrentPassword { get; set; } = null!;
}
