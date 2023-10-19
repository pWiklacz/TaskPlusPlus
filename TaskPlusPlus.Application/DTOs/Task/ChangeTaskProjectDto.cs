using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Task;
public class ChangeTaskProjectDto : BaseDto
{
    public ulong ProjectId { get; set; }
}
