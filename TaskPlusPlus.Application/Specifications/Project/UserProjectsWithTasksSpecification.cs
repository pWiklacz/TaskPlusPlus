namespace TaskPlusPlus.Application.Specifications.Project;
internal class UserProjectsWithTasksSpecification : Specification<Domain.Entities.Project>
{
    public UserProjectsWithTasksSpecification(string userId)
    : base(project => project.UserId == userId)
    {
        AddInclude(p=>p.Tasks);
        AddOrderBy(p => p.IsCompleted);
    }

    public UserProjectsWithTasksSpecification(ulong id, string userId)
        : base(project => (project.UserId == userId)
        && project.Id == id)
    {
        AddInclude(p => p.Tasks);
    }
}
