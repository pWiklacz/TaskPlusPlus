using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Domain.Enums;

namespace TaskPlusPlus.Application.DTOs.Task;

public class CreateTaskDto
{
    public string Name { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public TimeOnly? DurationTime { get; set; }
    public string Priority { get; set; } = null!;
    public string Energy { get; set; } = null!;
    public ulong? ProjectId { get; set; }
    public ulong CategoryId { get; set; }

    public List<TagDto> Tags = new();
        
}