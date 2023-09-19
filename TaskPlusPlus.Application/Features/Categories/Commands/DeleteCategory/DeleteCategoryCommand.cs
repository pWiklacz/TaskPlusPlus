using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Categories.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(ulong Id) : ICommand;

