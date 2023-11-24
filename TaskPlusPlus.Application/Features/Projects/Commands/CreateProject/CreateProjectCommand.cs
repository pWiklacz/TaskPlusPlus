using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(CreateProjectDto Dto) : ICommand<ulong>;

