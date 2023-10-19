using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskProject;
internal sealed class ChangeTaskProjectCommandHandler : ICommandHandler<ChangeTaskProjectCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeTaskProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(ChangeTaskProjectCommand request, CancellationToken cancellationToken)
    {
        var validator = new ChangeTaskProjectDtoValidator(_unitOfWork);
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

        task.ChangeProject(request.Dto.ProjectId);

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
 