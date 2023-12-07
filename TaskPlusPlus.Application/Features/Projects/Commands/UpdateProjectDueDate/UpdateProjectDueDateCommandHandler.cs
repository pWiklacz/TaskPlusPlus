using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Common.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectDueDate;
internal class UpdateProjectDueDateCommandHandler : ICommandHandler<UpdateProjectDueDateCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectDueDateCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProjectDueDateCommand request, CancellationToken cancellationToken)
    {
        var validator = new DueDateDtoValidator();
        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Project)));
        }

        var project = await _unitOfWork.Repository<Project, ProjectId>().GetByIdAsync(request.Dto.Id);

        if (project is null)
        {
            return Result.Fail(new NotFoundError(nameof(Project), request.Dto.Id));
        }

        var updateDueDateResult = project.UpdateDueDate((DateOnly)request.Dto.DueDate!);

        if (updateDueDateResult.IsFailed)
        {
            return updateDueDateResult;
        }
            
        _unitOfWork.Repository<Project, ProjectId>().Update(project);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Project)));
        }

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Project)));

    }
}
