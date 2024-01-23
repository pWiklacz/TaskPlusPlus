using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.DTOs;

public class UpdateCategorySettingsDto : BaseDto
{
    public CategorySettings Settings { get; set; } = null!;
}
