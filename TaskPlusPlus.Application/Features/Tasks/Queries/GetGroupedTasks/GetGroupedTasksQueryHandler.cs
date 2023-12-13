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
internal sealed class GetGroupedTasksQueryHandler : IQueryHandler<GetGroupedTasksQuery, Dictionary<string, List<TaskDto>>>
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
    public async Task<Result<Dictionary<string, List<TaskDto>>>> Handle(GetGroupedTasksQuery request, CancellationToken cancellationToken)
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

        var result = new Dictionary<string, List<TaskDto>>();

        switch (request.QueryParams.GroupBy)
        {
            case nameof(TaskDto.Tags):
                {
                    var groupedTasksDto = tasksDto
                        .SelectMany(task => task.Tags.DefaultIfEmpty(), (task, tag) => new { Task = task, Tag = tag })
                        .GroupBy(x => x.Tag != null ? x.Tag.Name : "No Label")
                        .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToList());

                    result = groupedTasksDto;
                    break;
                }
            case "":
                result["All"] = tasksDto;
                break;
            default:
                {

                    /**
                     * sortowanie po Name, DueDate, DurationTimeInMinutes, Priority, Energy, CreatedOn
                     * grupowanie po DueDate, DurationTimeInMinutes, Priority, Energy, CreatedOn 
                     */
                    var groupBy = typeof(TaskDto).GetProperty(request.QueryParams.GroupBy);

                    if (groupBy is not null)
                    {
                        var groupedTasksDto = tasksDto
                            .GroupBy(t => groupBy.GetValue(t) ?? "No Value")
                            .OrderBy(x => x.Key);

                        foreach (var group in groupedTasksDto)
                        {
                            var key = group.Key?.ToString() ?? "No Value";
                            result[key] = group.Select(item => item).ToList();
                        }
                    }
                    else
                    {
                        return Result.Fail(new GroupingProblemError(request.QueryParams.GroupBy));
                    }

                    break;
                }
        }

        return result;

    }
}
