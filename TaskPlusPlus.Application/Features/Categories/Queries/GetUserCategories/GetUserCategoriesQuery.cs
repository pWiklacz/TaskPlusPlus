using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetUserCategories;

public record GetUserCategoriesQuery() : IQuery<List<CategoryDto>>;

