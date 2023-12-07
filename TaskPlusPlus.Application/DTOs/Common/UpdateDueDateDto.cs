using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Common;
public class UpdateDueDateDto : BaseDto, IDueDateDto
{
    public DateOnly? DueDate { get; set; }
}
