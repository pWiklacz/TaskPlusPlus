using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.DTOs.Category;
public class CategoryDto : BaseDto, ICategoryDto
{
    public string Name { get; set; } = null!;
    public bool IsFavorite { get;  set; }
    public bool IsImmutable { get; set; }
    public string ColorHex { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public CategorySettings Settings { get; set; } = null!;
}
