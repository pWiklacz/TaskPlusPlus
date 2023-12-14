using System.Linq.Expressions;
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
            switch (sortBy.Name)
            {
                case "DurationTimeInMinutes":
                    if (queryParams.SortDescending)
                        AddOrderByDescending(t => t.DurationTimeInMinutes);
                    else
                        AddOrderBy(t => t.DurationTimeInMinutes!);
                    break;
                case "CreatedOnUtc":
                    if (queryParams.SortDescending)
                        AddOrderByDescending(t => t.CreatedOnUtc);
                    else
                        AddOrderBy(t => t.CreatedOnUtc!);
                    break;
                default:
                    var parameter = Expression.Parameter(typeof(Domain.Entities.Task), "t");
                    var property = Expression.Property(parameter, sortBy);
                    var lambda = Expression.Lambda<Func<Domain.Entities.Task, object>>(property, parameter);

                    if (queryParams.SortDescending)
                        AddOrderByDescending(lambda);
                    else
                        AddOrderBy(lambda);
                    break;
            }
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

