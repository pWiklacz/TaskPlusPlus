using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.DTOs.Task;

namespace TaskPlusPlus.Application.DTOs.Project;

public sealed class ProjectDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public string ColorHex { get; set; } = null!;
    public string UserId { get; set; } = null!;

    public List<TaskDto> Tasks = new();
    public List<TagDto> Tags = new();
}