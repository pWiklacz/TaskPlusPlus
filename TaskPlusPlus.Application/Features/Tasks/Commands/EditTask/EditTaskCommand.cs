using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.EditTask;

public sealed record EditTaskCommand(EditTaskDto Dto) : ICommand;
