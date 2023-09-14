namespace TaskPlusPlus.Application.DTOs.Tag;

public sealed class CreateTagDto
{
    public string Name { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public bool IsFavorite { get; set; }
}