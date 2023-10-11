using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetCategories;

public record GetCategoriesQuery() : IQuery<List<CategoryDto>>;

