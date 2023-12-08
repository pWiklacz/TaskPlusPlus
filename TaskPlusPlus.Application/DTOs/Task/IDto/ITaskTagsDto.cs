using TaskPlusPlus.Application.DTOs.Tag;

namespace TaskPlusPlus.Application.DTOs.Task.IDto;
internal interface ITaskTagsDto
{
    public List<ulong> Tags { get; set; }
}
