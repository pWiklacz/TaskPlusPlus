using TaskPlusPlus.Application.DTOs.Base;
using TaskPlusPlus.Application.DTOs.Task;

namespace TaskPlusPlus.Application.DTOs.Project;

public sealed class ProjectDto : BaseDto, IProjectDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public string UserId { get; set; } = null!;
    public ulong CategoryId { get; set; }
    public DateTime? CompletedOnUtc { get; set; }

    public List<TaskDto> Tasks = new();
}