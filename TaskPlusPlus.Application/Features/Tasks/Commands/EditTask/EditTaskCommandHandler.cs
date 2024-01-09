using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Application.Specifications.Task;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.EditTask;
internal sealed class EditTaskCommandHandler : ICommandHandler<EditTaskCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public EditTaskCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var dto = request.Dto;
        var userId = userResult.Value.Id;
        var spec = new TasksWithTagsSpecification(dto.Id, userId);
        var task = await _unitOfWork.Repository<Task, TaskId>().GetEntityWithSpec(spec);

        if (task is null)
        {
            return Result.Fail(new NotFoundError(nameof(Task), dto.Id));
        }

        var dateChanged = false;
        if (dto.DueDate.HasValue)
        {
            dateChanged = dto.DueDate != task.DueDate?.Value;
        }
        var validator = new EditTaskDtoValidator(_unitOfWork, dateChanged);
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Task)));
        }

        var errors = new List<IError>();

        var updateNameResult = task.UpdateName(dto.Name);
        if (updateNameResult.IsFailed)
            errors.AddRange(updateNameResult.Errors);
        var updateNotesResult = task.UpdateNotes(dto.Notes);
        if (updateNotesResult.IsFailed)
            errors.AddRange(updateNotesResult.Errors);
        var updateDueDateResult = task.UpdateDueDate(dto.DueDate);
        if (updateDueDateResult.IsFailed)
            errors.AddRange(updateDueDateResult.Errors);
        var energyUpdateResult = Energy.FromValue(request.Dto.Energy);
        if (energyUpdateResult.IsFailed)
            errors.AddRange(energyUpdateResult.Errors);
        var priorityUpdateResult = Priority.FromValue(request.Dto.Priority);
        if (priorityUpdateResult.IsFailed)
            errors.AddRange(priorityUpdateResult.Errors);
        task.ChangePriority(priorityUpdateResult.Value!);
        task.ChangeEnergy(energyUpdateResult.Value!);
        task.UpdateDurationTime(dto.DurationTime);
        task.UpdateDueTime(dto.DueTime);
        task.ChangeProject(dto.ProjectId);
        task.ChangeCategory(dto.CategoryId);

        var tagsIds = request.Dto.Tags;
        var tags = new List<Tag>();

        foreach (var tagId in tagsIds)
        {
            var tag = await _unitOfWork.Repository<Tag, TagId>().GetByIdAsync(tagId);
            tags.Add(tag!);
        }

        var updateTagsResult = task.UpdateTags(tags);
        if (updateTagsResult.IsFailed)
            errors.AddRange(updateTagsResult.Errors);

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
