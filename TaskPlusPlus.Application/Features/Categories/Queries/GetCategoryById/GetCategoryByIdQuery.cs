using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(ulong Id) : IQuery<CategoryDto>;

