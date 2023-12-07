using System.Linq.Expressions;
using System.Reflection;
using TaskPlusPlus.Application.Helpers;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;

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
            switch (sortBy.PropertyType)
            {
                case { } type when type == typeof(TimeOnly?):
                    if (queryParams.SortDescending)
                        AddOrderByDescending(t => t.DueTime!);
                    else
                        AddOrderBy(t => t.DueTime!);
                    break;
                case { } type when type == typeof(ProjectId?):
                    if (queryParams.SortDescending)
                        AddOrderByDescending(t => t.ProjectId!);
                    else
                        AddOrderBy(t => t.ProjectId!);
                    break;
                case { } type when type == typeof(CategoryId):
                    if (queryParams.SortDescending)
                        AddOrderByDescending(t => t.CategoryId);
                    else
                        AddOrderBy(t => t.CategoryId);
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

