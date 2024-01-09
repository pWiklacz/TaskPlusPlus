using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Commands.EditProject;
public sealed record EditProjectCommand(EditProjectDto Dto) : ICommand;