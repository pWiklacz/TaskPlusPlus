namespace TaskPlusPlus.Application.DTOs.Project;
public sealed class CreateProjectDto : IProjectDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public TimeOnly? DueTime { get; set; }
}
