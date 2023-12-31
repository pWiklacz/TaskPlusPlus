﻿using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, ulong>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateTaskCommandHandler(
        IUnitOfWork unitOfWork,
      IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<ulong>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;

        var dto = request.Dto;
        var validator = new CreateTaskDtoValidator(_unitOfWork);

        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Task)));
        }

        var result = Task.Create(dto.Name, dto.DueDate, dto.Notes, dto.Priority, dto.ProjectId,
            dto.Energy, dto.DueTime, userId, dto.CategoryId, dto.DurationTime);

        if (result.IsFailed)
            return result.ToResult();

        var task = result.Value;
        var tagsIds = dto.Tags;

        foreach (var tagId in tagsIds)
        {
            var tag = await _unitOfWork.Repository<Tag, TagId>().GetByIdAsync(tagId);
            task.AddTag(tag!);
        }

        _unitOfWork.Repository<Task, TaskId>().Add(task);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new CreatingProblemError(nameof(Task)));
        }

        return Result.Ok(task.Id.Value)
            .WithSuccess(new CreationSuccess(nameof(Task)));
    }
}