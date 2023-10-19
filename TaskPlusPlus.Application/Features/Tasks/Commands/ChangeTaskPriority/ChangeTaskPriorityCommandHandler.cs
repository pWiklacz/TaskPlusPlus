using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskPriority;
internal sealed class ChangeTaskPriorityCommandHandler : ICommandHandler<ChangeTaskPriorityCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeTaskPriorityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(ChangeTaskPriorityCommand request, CancellationToken cancellationToken)
    {
        var validator = new TaskPriorityDtoValidator();
        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Task)));
        }

        var task = await _unitOfWork.Repository<Task, TaskId>().GetByIdAsync(request.Dto.Id);

        if (task == null)
        {
            return Result.Fail(new NotFoundError(nameof(Task), request.Dto.Id));
        }

        var priorityUpdateResult = Priority.FromName(request.Dto.Priority);
        if (priorityUpdateResult.IsFailed)
            return priorityUpdateResult.ToResult();

        _unitOfWork.Repository<Task, TaskId>().Update(task);
        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Task)));
        }

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Task)));
    }
}
