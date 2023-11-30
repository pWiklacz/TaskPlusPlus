using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Tag;

namespace TaskPlusPlus.Application.DTOs.Task;

public class TaskDto : BaseDto
{
    public string Name { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public TimeOnly? DurationTime { get; set; }
    public int Priority { get; set; } 
    public int Energy { get; set; } 
    public ulong? ProjectId { get; set; }
    public ulong CategoryId { get; set; }
    public DateTime? CompletedOnUtc { get; set; }
    public List<TagDto> Tags { get; set; } = new();
}