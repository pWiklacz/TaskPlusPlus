using TaskPlusPlus.Application.DTOs.Common;

namespace TaskPlusPlus.Application.DTOs.Category;
public class UpdateCategoryDto : BaseDto, ICategoryDto
{
    public string Name { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public string Icon { get; set; } = null!;
}
