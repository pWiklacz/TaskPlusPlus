namespace TaskPlusPlus.Application.DTOs.Project;
public sealed class CreateProjectDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public string ColorHex { get; set; } = null!;
}
