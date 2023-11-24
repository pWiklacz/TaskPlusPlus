namespace TaskPlusPlus.Application.DTOs.Category;
public class CreateCategoryDto : ICategoryDto
{
    public string Name { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public string ColorHex { get; set; } = null!;
    public string Icon { get; set; } = null!;
}
