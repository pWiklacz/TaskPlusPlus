using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Task.IDto;

namespace TaskPlusPlus.Application.DTOs.Task;
public class ChangeTaskEnergyDto : BaseDto, ITaskEnergyDto
{
    public int Energy { get; set; }
}
