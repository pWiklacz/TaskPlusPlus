using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Common.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskDueDate;
internal class UpdateTaskDueDateCommandHandler : ICommandHandler<UpdateTaskDueDateCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskDueDateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTaskDueDateCommand request, CancellationToken cancellationToken)
    {
        var validator = new DueDateDtoValidator();
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

        var updateDueDateResult = task.UpdateDueDate((DateTime)request.Dto.DueDate!);

        if (updateDueDateResult.IsFailed)
        {
            return updateDueDateResult;
        }

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
