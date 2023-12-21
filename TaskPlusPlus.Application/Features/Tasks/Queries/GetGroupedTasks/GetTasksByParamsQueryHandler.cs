using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Extensions;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Specifications.Task;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetGroupedTasks;
internal sealed class GetTasksByParamsQueryHandler : IQueryHandler<GetTasksByParamsQuery, Dictionary<string, List<TaskDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetTasksByParamsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<Result<Dictionary<string, List<TaskDto>>>> Handle(GetTasksByParamsQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;
        ISpecification<Task> spec;

        if (request.QueryParams.Date != null)
        {
            spec = new TasksWithTagsByDateSpecification(request.QueryParams, userId);
        }
        else
        {
            spec = new TasksWithTagsSpecification(request.QueryParams, userId);
        }

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
            case "None":
                result["All"] = tasksDto;
                break;
            case "DurationTime":
                result = tasksDto
                    .GroupBy(task => task.DurationTime)
                    .ToDictionary(g => "Duration time: " + g.Key.FormatMinutes(), g => g.ToList());
                break;
            case "DueDate":
                var today = DateOnly.FromDateTime(DateTime.Now);

                result = tasksDto
                    .GroupBy(task =>
                    {
                        if (task.DueDate == null)
                        {
                            return "NoDueDate";
                        }
                        return task.DueDate.Value < today ? "Overdue" : task.DueDate.ToString();
                    })
                    .ToDictionary(g => g.Key!, g => g.ToList());
                break;
            case "Priority":
                result = tasksDto
                    .GroupBy(task => task.Priority)
                    .ToDictionary(g => "Priority: " + Priority.FromValue(g.Key).Value, g => g.ToList());
                break;
            case "Energy":
                result = tasksDto
                    .GroupBy(task => task.Energy)
                    .ToDictionary(g => "Energy: " + Energy.FromValue(g.Key).Value, g => g.ToList());
                break;
            default:
                return Result.Fail(new GroupingProblemError(request.QueryParams.GroupBy));
        }
        return result;
    }
}
