using FluentResults;
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
    public int DurationTimeInMinutes { get; private set; }
    public TimeOnly? DueTime { get; private set; }
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
    { }

    private Task(
        TaskName name,
        DueDate? dueDate,
        Notes notes,
        Priority priority,
        ProjectId? projectId,
        Energy energy,
        TimeOnly? dueTime,
        UserId userId,
        CategoryId categoryId,
        int durationTime)
    {
        Name = name;
        DueDate = dueDate;
        Notes = notes;
        Priority = priority;
        ProjectId = projectId;
        Energy = energy;
        DueTime = dueTime;
        UserId = userId;
        CategoryId = categoryId;
        DurationTimeInMinutes = durationTime;
        IsCompleted = false;
    }

    public static Result<Task> Create(
        string name,
        DateOnly? dueDate,
        string notes,
        string priority,
        ProjectId? projectId,
        string energy,
        TimeOnly? dueTime,
        string userId,
        CategoryId categoryId,
        int durationTime)
    {
        var errors = new List<IError>();

        var nameResult = TaskName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        Result<DueDate> dueDateResult = null!;
        if (dueDate != null)
        {
            dueDateResult = DueDate.Create((DateOnly)dueDate);
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
        if (energyResult.IsFailed)
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
            dueTime,
            userIdResult.Value,
            categoryId,
            durationTime);

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
    public Result UpdateTags(IEnumerable<Tag> tags)
    {
        var enumerable = tags.ToList();

        var tagsToDelete = _tags.Except(enumerable).ToList();

        var errors = new List<IError>();

        foreach (var deleteResult in tagsToDelete.Select(DeleteTag).Where(deleteResult => deleteResult.IsFailed))
        {
            errors.AddRange(deleteResult.Errors);
        }

        if (errors.Any())
            return Result.Fail(errors);

        foreach (var tag in enumerable)
        {
            AddTag(tag);
        }

        return Result.Ok();
    }
    public Result DeleteTag(Tag tag)
    {
        var tagResult = GetTag(tag.Id);

        if (tagResult.IsFailed)
            return Result.Fail(tagResult.Errors);

        _tags.Remove(tagResult.Value);
        return Result.Ok();
    }

    public void ChangePriority(Priority priority)
    => Priority = priority;
    public Result UpdateDueDate(DateOnly dueDate)
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
    public void ChangeCompleteState(bool isCompleted)
    {
        switch (IsCompleted)
        {
            case false:
                if (!isCompleted)
                    break;
                IsCompleted = isCompleted;
                CompletedOnUtc = DateTime.UtcNow;
                break;
            case true:
                if (isCompleted)
                    break;
                IsCompleted = isCompleted;
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