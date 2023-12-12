using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Task.IDto;

namespace TaskPlusPlus.Application.DTOs.Task;
public class ChangeTaskPriorityDto : BaseDto, ITaskPriorityDto
{
    public int Priority { get; set; }
}
