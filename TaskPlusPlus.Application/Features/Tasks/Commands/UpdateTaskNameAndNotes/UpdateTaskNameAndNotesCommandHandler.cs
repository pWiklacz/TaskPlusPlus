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

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskNameAndNotes;

internal sealed class UpdateTaskNameAndNotesCommandHandler : ICommandHandler<UpdateTaskNameAndNotesCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskNameAndNotesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateTaskNameAndNotesCommand request, CancellationToken cancellationToken)
    {
        var validator = new NameAndNotesValidator();
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

        var errors = new List<IError>();

        var updateNameResult = task.UpdateName(request.Dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);

        var updateNotesResult = task.UpdateNotes(request.Dto.Notes);
        if (updateNotesResult.IsFailed)
            errors.AddRange(updateNotesResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

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

