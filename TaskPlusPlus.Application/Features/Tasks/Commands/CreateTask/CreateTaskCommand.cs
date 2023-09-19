using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(CreateTaskDto Dto) : ICommand;
