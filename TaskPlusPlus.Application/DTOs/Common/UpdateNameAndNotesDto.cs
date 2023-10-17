using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Common;
public class UpdateNameAndNotesDto : BaseDto, INameAndNotesDto
{
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = string.Empty;
}