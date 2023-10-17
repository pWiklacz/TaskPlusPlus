using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Common.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectNameAndNotes;
internal sealed class UpdateProjectNameAndNotesCommandHandler : ICommandHandler<UpdateProjectNameAndNotesCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectNameAndNotesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProjectNameAndNotesCommand request, CancellationToken cancellationToken)
    {
        var validator = new NameAndNotesValidator();
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

        var errors = new List<IError>();

        var updateNameResult = project.UpdateName(request.Dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);

        var updateNotesResult = project.UpdateNotes(request.Dto.Notes);
        if (updateNotesResult.IsFailed)
            errors.AddRange(updateNotesResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

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
