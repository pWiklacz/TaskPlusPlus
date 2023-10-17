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
    public string Priority { get; set; } = null!;
    public string Energy { get; set; } = null!;
    public ulong? ProjectId { get; set; }
    public ulong CategoryId { get; set; }
    public DateTime? CompletedOnUtc { get; set; }

    public List<TagDto> Tags = new();
}