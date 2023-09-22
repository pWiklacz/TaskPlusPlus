﻿using FluentResults;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

public sealed class Task : Entity<TaskId>, IAuditEntity
{
    public TaskName Name { get; private set; } = null!;
    public DueDate? DueDate { get; private set; }
    public Notes Notes { get; private set; } = null!;
    public bool IsCompleted { get; private set; }
    public TimeOnly? DurationTime { get; private set; }
    public Priority Priority { get; private set; } = null!;
    public Energy Energy { get; private set; } = null!;
    public ProjectId? ProjectId { get; private set; }
    public UserId UserId { get; private set; } = null!;
    public CategoryId CategoryId { get; private set; }
    public IReadOnlyCollection<Tag> Tags => _tags;

    private readonly List<Tag> _tags = new();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public DateTime? CompletedOnUtc { get; private set; }

    private Task()
    {}

    private Task(
        TaskName name,
        DueDate? dueDate,
        Notes notes,
        Priority priority, 
        ProjectId? projectId, 
        Energy energy,
        TimeOnly? durationTime,
        UserId userId, 
        CategoryId categoryId)
    {
        Name = name;
        DueDate = dueDate;
        Notes = notes;
        Priority = priority;
        ProjectId = projectId;
        Energy = energy;
        DurationTime = durationTime;
        UserId = userId;
        CategoryId = categoryId;
        IsCompleted = false;
    }

    public static Result<Task> Create(
        string name,
        DateTime? dueDate,
        string notes,  
        string priority,
        ProjectId? projectId,
        string energy,
        TimeOnly? durationTime,
        string userId,
        CategoryId categoryId)
    {
        var errors = new List<IError>();

        var nameResult = TaskName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        Result<DueDate> dueDateResult = null!;
        if (dueDate != null)
        {
            dueDateResult = DueDate.Create((DateTime)dueDate);
            if (dueDateResult.IsFailed)
                errors.AddRange(dueDateResult.Errors);
        }

        var userIdResult = UserId.Create(userId);
        if (userIdResult.IsFailed)
            errors.AddRange(userIdResult.Errors);

        var notesResult = Notes.Create(notes);
        if (notesResult.IsFailed)
            errors.AddRange(notesResult.Errors);

        var priorityResult = Priority.FromName(priority);
        if (priorityResult.IsFailed)
            errors.AddRange(priorityResult.Errors);

        var energyResult = Energy.FromName(energy);
        if(energyResult.IsFailed)
            errors.AddRange(energyResult.Errors);

        if (errors.Any())
            return Result.Fail<Task>(errors);

        var task = new Task(
            nameResult.Value,
            dueDate == null ? null : dueDateResult!.Value,
            notesResult.Value,
            priorityResult.Value!,
            projectId,
            energyResult.Value!,
            durationTime,
            userIdResult.Value,
            categoryId);

        return task;
    }

    public void ChangeCategory(CategoryId categoryId)
        => CategoryId = categoryId;

    public void ChangeProject(ProjectId projectId)
        => ProjectId = projectId;
    
    public Result AddTag(Tag tag)
    {
        var alreadyExist = _tags.Any(t => t == tag);
        if (alreadyExist)
            return Result.Ok();
        _tags.Add(tag);
        return Result.Ok();
    }
    public void AddTags(IEnumerable<Tag> tags)
    {
        foreach (var tag in tags)
        {
            AddTag(tag);
        }
    }
    public Result DeleteTag(Tag tag)
    {
        var tagResult = GetTag(tag.Id);

        if(tagResult.IsFailed)
            return Result.Fail(tagResult.Errors);

        _tags.Remove(tagResult.Value);
        return Result.Ok();
    }
    
    public void ChangePriority(Priority priority)
    => Priority = priority;
    public Result UpdateDueDate(DateTime dueDate)
    {
        var dueDateResult = DueDate.Create(dueDate);
        if (dueDateResult.IsFailed)
            return Result.Fail(dueDateResult.Errors);
        DueDate = dueDateResult.Value;
        return Result.Ok();
    }
    public Result UpdateName(string name)
    {
        var taskNameResult = TaskName.Create(name);
        if (taskNameResult.IsFailed)
            return Result.Fail(taskNameResult.Errors);
        Name = taskNameResult.Value;
        return Result.Ok();
    }
    public Result UpdateNotes(string notes)
    {
        var notesResult = Notes.Create(notes);
        if (notesResult.IsFailed)
            return Result.Fail(notesResult.Errors);
        Notes = notesResult.Value;
        return Result.Ok();
    }
    public void ChangeCompleteState()
    {
        switch (IsCompleted)
        {
            case false:
                IsCompleted = true;
                CompletedOnUtc = DateTime.UtcNow;
                break;
            case true:
                IsCompleted = false;
                CompletedOnUtc = null;
                break;
        }
    }
    private Result<Tag> GetTag(TagId id)
    {
        var tag = _tags.SingleOrDefault(t => t.Id == id);

        return tag ?? Result.Fail<Tag>(
            new NotFoundError(nameof(Tag), id));
    }
    
} 