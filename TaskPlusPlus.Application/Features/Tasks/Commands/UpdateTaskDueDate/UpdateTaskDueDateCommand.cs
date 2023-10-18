using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskDueDate;
public record UpdateTaskDueDateCommand(UpdateDueDateDto Dto) : ICommand;
