using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task;

public class TaskDto : BaseDto
{
    public string Name { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public TimeOnly? DurationTime { get; set; }
    public Priority Priority { get; set; } = null!;
    public Energy Energy { get; set; } = null!;
    public ulong? ProjectId { get; set; }
    public string UserId { get; set; } = null!;
    public ulong CategoryId { get; set; }
    public DateTime? CompletedOnUtc { get; set; }

    public List<TaskDto> SubTasks = new();
    public List<TagDto> Tags = new();
}