using System.ComponentModel.DataAnnotations;

namespace TaskPlusPlus.Application.Models.Identity;

public class ExternalAuthRequest
{
    [Required] public string Email { get; set; } = null!;

    [Required] public string Provider { get; set; } = null!;

    [Required] public string AccessToken { get; set; } = null!;

    [Required] public string FirstName { get; set; } = null!;

    public string? LastName { get; set; } = String.Empty;

}