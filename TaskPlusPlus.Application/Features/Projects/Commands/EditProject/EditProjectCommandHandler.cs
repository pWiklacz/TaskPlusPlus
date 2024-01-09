using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Project.Validators;
using TaskPlusPlus.Application.Features.Tasks.Commands.EditTask;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Application.Specifications.Project;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Commands.EditProject;

internal sealed class EditProjectCommandHandler : ICommandHandler<EditProjectCommand>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public EditProjectCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(EditProjectCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var dto = request.Dto;
        var userId = userResult.Value.Id;

        var spec = new UserProjectsWithTasksSpecification(dto.Id, userId);
        var project = await _unitOfWork.Repository<Project, ProjectId>().GetEntityWithSpec(spec);

        if (project is null)
        {
            return Result.Fail(new NotFoundError(nameof(Project), dto.Id));
        }

        var dateChanged = false;
        if (dto.DueDate.HasValue)
        {
            dateChanged = dto.DueDate != project.DueDate?.Value;
        }

        var validator = new EditProjectDtoValidator(dateChanged);

        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Project)));
        }

        var errors = new List<IError>();

        var updateNameResult = project.UpdateName(dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);
        var updateNotesResult = project.UpdateNotes(dto.Notes);
        if (updateNotesResult.IsFailed)
            errors.AddRange(updateNotesResult.Errors);
        var updateDueDateResult = project.UpdateDueDate(dto.DueDate);
        if (updateDueDateResult.IsFailed)
            errors.AddRange(updateDueDateResult.Errors);
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

