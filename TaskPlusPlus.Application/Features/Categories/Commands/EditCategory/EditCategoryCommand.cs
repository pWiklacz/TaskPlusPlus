using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Commands.EditCategory;

public sealed record EditCategoryCommand(UpdateCategoryDto Dto) : ICommand;

