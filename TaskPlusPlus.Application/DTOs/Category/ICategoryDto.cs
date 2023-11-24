namespace TaskPlusPlus.Application.DTOs.Category;
public interface ICategoryDto
{
    public string Name { get; set; }
    public string ColorHex { get; set; }
    public bool IsFavorite { get; set; }
    public string Icon { get; set; } 
}
