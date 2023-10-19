using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskTags;

public record UpdateTaskTagsCommand(UpdateTaskTagsDto Dto) : ICommand;

