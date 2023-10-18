using System.Reflection;
using TaskPlusPlus.Application.Helpers;

namespace TaskPlusPlus.Application.Specifications.Task;

internal class TasksWithTagsSpecification : Specification<Domain.Entities.Task>  
{
    public TasksWithTagsSpecification(TaskQueryParameters queryParams, string userId)
        : base(task =>
            (string.IsNullOrEmpty(queryParams.Search) || task.Name.Value.ToLower().Contains(queryParams.Search)) &&
            (task.CategoryId == queryParams.CategoryId)
            && task.UserId == userId)
    {
        AddInclude(t => t.Tags);
        AddOrderBy(t => t.Name);

        var sortBy = typeof(Domain.Entities.Task).GetProperty(queryParams.SortBy);
        if (sortBy is not null)
        {
            if (queryParams.SortDescending)
                AddOrderByDescending(t => sortBy.GetValue(t)!);
            else
                AddOrderBy(t => sortBy.GetValue(t)!);
        }
    }

    public TasksWithTagsSpecification(ulong id, string userId) 
        : base(t => 
            (t.Id == id)
            && t.UserId == userId)
    {
        AddInclude(t => t.Tags);
    }
}

