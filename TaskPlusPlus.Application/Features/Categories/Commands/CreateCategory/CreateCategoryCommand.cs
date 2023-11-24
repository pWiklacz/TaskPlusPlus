using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(CreateCategoryDto Dto) : ICommand<ulong>;

