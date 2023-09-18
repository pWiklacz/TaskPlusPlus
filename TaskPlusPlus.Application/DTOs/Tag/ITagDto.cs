namespace TaskPlusPlus.Application.DTOs.Tag;

public interface ITagDto
{
    public string Name { get; set; } 
    public string ColorHex { get; set; }
    public bool IsFavorite { get; set; }
}