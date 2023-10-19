using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskProject;

public record ChangeTaskProjectCommand(ChangeTaskProjectDto Dto) : ICommand;

