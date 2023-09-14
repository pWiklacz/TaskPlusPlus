using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Persistence.Repositories;

internal sealed class ProjectRepository : GenericRepository<Project, ProjectId>, IProjectRepository
{
    public ProjectRepository(TaskPlusPlusDbContext dbContext) : base(dbContext)
    {
    }
}