using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Tag;

public sealed class TagDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public bool IsFavorite { get; set; }
}