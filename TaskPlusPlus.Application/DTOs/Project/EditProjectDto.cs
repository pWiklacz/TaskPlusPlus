using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Project;
public class EditProjectDto: BaseDto, IProjectDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
    public DateOnly? DueDate { get; set; }
    public TimeOnly? DueTime { get; set; }
    public bool IsCompleted { get; set; }
}
