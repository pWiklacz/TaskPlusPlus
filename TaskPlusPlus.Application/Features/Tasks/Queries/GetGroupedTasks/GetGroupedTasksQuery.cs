using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Helpers;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetGroupedTasks;

public record GetGroupedTasksQuery(TaskQueryParameters QueryParams)
    : IQuery<Dictionary<object, List<TaskDto>>>;

