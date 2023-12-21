using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Task;
public class CalendarTaskDto : BaseDto
{
    public string Name { get; set; } = null!;
    public DateOnly DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public ulong? ProjectId { get; set; }
    public ulong CategoryId { get; set; }
}
