using TaskPlusPlus.Application.DTOs.Common.IDto;
using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Task.IDto;

namespace TaskPlusPlus.Application.DTOs.Task;
public class EditTaskDto : BaseDto, ITaskTagsDto, IDueDateDto, INameAndNotesDto, ITaskPriorityDto, ITaskEnergyDto
{
    public string Name { get; set; } = null!;
    public DateOnly? DueDate { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public TimeOnly? DueTime { get; set; }
    public int DurationTime { get; set; }
    public int Priority { get; set; }
    public int Energy { get; set; }
    public ulong? ProjectId { get; set; }
    public ulong CategoryId { get; set; }
    public List<ulong> Tags { get; set; } = new();
}
