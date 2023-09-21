using TaskPlusPlus.Application.DTOs.Base;

namespace TaskPlusPlus.Application.DTOs.Category;
public class CategoryDto : BaseDto, ICategoryDto
{
    public string Name { get; set; } = null!;
    public bool IsImmutable { get;  set; }
    public bool IsFavorite { get;  set; }
    public string UserId { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
}
