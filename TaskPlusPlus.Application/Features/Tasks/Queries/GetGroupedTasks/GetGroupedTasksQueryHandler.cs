using AutoMapper;
using FluentResults;
using System.Reflection;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Specifications.Task;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetGroupedTasks;
internal sealed class GetGroupedTasksQueryHandler : IQueryHandler<GetGroupedTasksQuery, Dictionary<object, List<TaskDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetGroupedTasksQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<Result<Dictionary<object, List<TaskDto>>>> Handle(GetGroupedTasksQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;
        var spec = new TasksWithTagsSpecification(request.QueryParams, userId);
        var tasksFromDb = await _unitOfWork.Repository<Task, TaskId>().ListAsync(spec);
        var tasksDto = _mapper.Map<List<TaskDto>>(tasksFromDb);

        var result = new Dictionary<object, List<TaskDto>>();

        switch (request.QueryParams.GroupBy)
        {
            case nameof(TaskDto.Tags):
                {
                    var groupedTasksDto = tasksDto
                        .SelectMany(task => task.Tags.Any()
                            ? task.Tags.Select(tag => new { Task = task, Tag = tag })
                            : new[] { new { Task = task, Tag = new TagDto { Id = 0, Name = "No Label", ColorHex = string.Empty } } })
                        .GroupBy(item => item.Tag);

                    foreach (var group in groupedTasksDto)
                    {
                        result[group.Key.Name] = group.Select(item => item.Task).ToList();
                    }

                    break;
                }
            case "":
                return new Dictionary<object, List<TaskDto>> { { "All", tasksDto } };
            default:
                {
                    var groupBy = typeof(TaskDto).GetProperty(request.QueryParams.GroupBy);

                    if (groupBy is not null)
                    {
                        var groupedTasksDto = tasksDto
                            .GroupBy(t => groupBy.GetValue(t)!)
                            .OrderBy(x => x.Key);

                        foreach (var group in groupedTasksDto)
                        {
                            result[group.Key] = group.Select(item => item).ToList();
                        }
                    }
                    else return Result.Fail(new GroupingProblemError(request.QueryParams.GroupBy));

                    break;
                }
        }

        return result;
    }
}
