using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(ulong Id) : ICommand;
