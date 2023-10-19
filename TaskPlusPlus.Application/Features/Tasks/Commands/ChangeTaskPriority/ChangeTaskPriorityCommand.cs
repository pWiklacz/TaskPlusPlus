using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskPriority;

public record ChangeTaskPriorityCommand(ChangeTaskPriorityDto Dto) : ICommand;
