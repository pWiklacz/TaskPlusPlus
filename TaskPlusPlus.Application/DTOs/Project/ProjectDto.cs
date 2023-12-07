using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Task;

namespace TaskPlusPlus.Application.DTOs.Project;

public sealed class ProjectDto : BaseDto, IProjectDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public TimeOnly? DueTime { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedOnUtc { get; set; }

    public List<TaskDto> Tasks = new();
}