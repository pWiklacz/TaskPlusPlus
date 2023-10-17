using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Queries.GetProjectById;
public record GetProjectByIdQuery(ulong Id) : IQuery<ProjectDto>;

