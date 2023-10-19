using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskCategory;

public record ChangeTaskCategoryCommand(ChangeTaskCategoryDto Dto) : ICommand;

