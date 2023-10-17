using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectDueDate;

public record UpdateProjectDueDateCommand(UpdateDueDateDto Dto) : ICommand;
