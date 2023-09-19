using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(ulong Id) : ICommand;

