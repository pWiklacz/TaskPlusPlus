using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;
public class UpdateUserDataRequest
{
    public string UserId { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
}
