using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Category;
public class CategoryDto : BaseDto, ICategoryDto
{
    public string Name { get; set; } = null!;
    public bool IsFavorite { get;  set; }
    public string ColorHex { get; set; } = null!;
}
