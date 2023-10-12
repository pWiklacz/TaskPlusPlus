using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Specifications.Task;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Queries.GetTaskById;
internal sealed class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new TasksWithTagsSpecification(request.Id);
        var taskFromDb = await _unitOfWork.Repository<Task, TaskId>().GetEntityWithSpec(spec);
        return _mapper.Map<TaskDto>(taskFromDb);
    }
}
