using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Common;
public class UpdateDueDateDto : BaseDto, IDueDateDto
{
    public DateTime? DueDate { get; set; }
}
