using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.DTOs.Task.IDto;

namespace TaskPlusPlus.Application.DTOs.Task;
public class UpdateTaskTagsDto : BaseDto, ITaskTagsDto
{
    public List<TagDto> Tags { get; set; } = new();
}
