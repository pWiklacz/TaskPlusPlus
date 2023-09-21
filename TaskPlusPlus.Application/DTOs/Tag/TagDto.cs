using TaskPlusPlus.Application.DTOs.Base;

namespace TaskPlusPlus.Application.DTOs.Tag;

public sealed class TagDto : BaseDto, ITagDto
{
    public string Name { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool IsFavorite { get; set; }
}