using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Helpers;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetGroupedTasks;

public record GetTasksByParamsQuery(TaskQueryParameters QueryParams)
    : IQuery<Dictionary<string, List<TaskDto>>>;

