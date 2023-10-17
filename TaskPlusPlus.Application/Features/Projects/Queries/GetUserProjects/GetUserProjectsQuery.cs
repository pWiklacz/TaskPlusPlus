using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Queries.GetUserProjects;

public record GetUserProjectsQuery() : IQuery<List<ProjectDto>>;

