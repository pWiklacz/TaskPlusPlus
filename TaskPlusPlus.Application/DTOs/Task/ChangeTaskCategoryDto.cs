using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Task;
public class ChangeTaskCategoryDto : BaseDto
{
    public ulong CategoryId { get; set; }
}
