using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(ulong Id) : IQuery<TaskDto>;
