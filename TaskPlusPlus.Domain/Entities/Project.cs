using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

public sealed class Project : Entity<ProjectId>
{
    //public override ProjectId Id { get; }
    public ProjectName Name { get; private set; }
    public Notes Notes { get; private set; }
    public DueDate? DueDate { get; private set; }
    public CreationTime CreationTime { get; private set; }
    public LastModifiedTime LastModifiedTime { get; private set; }
    public bool IsCompleted { get; private set; }
    public ColorHex ColorHex { get; private set; }
    public IReadOnlyCollection<Tag> Tags => _tags;
    public IReadOnlyCollection<Task> Tasks => _tasks;
    public UserId UserId { get; private set; }

    private readonly List<Task> _tasks = new();
    private readonly List<Tag> _tags = new();

    public Project(
        ProjectName name,
        Notes notes, 
        DueDate? dueDate, 
        CreationTime creationTime, 
        LastModifiedTime lastModifiedTime,
        ColorHex colorHex,
        UserId userId)
    {
        Name = name;
        Notes = notes;
        DueDate = dueDate;
        CreationTime = creationTime;
        LastModifiedTime = lastModifiedTime;
        ColorHex = colorHex;
        UserId = userId;
        IsCompleted = false;
    }

    public Result<Project> Create(
        string name,
        string notes,
        DateTime? dueDate,
        string colorHex,
        string userId)
    {

        var errors = new List<IError>();

        var creationTimeResult = CreationTime.Create(DateTime.Now);
        if (creationTimeResult.IsFailed)
            errors.AddRange(creationTimeResult.Errors);

        var lastModifiedTimeResult = LastModifiedTime.Create(creationTimeResult.Value);
        if (lastModifiedTimeResult.IsFailed)
            errors.AddRange(lastModifiedTimeResult.Errors);

        var userIdResult = UserId.Create(userId);
        if (userIdResult.IsFailed)
            errors.AddRange(userIdResult.Errors);

        var nameResult = ProjectName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        Result<DueDate> dueDateResult = null!;
        if (dueDate != null)
        {
            dueDateResult = DueDate.Create((DateTime)dueDate);
            if (dueDateResult.IsFailed)
                errors.AddRange(dueDateResult.Errors);
        }

        var notesResult = Notes.Create(notes);
        if (notesResult.IsFailed)
            errors.AddRange(notesResult.Errors);

        var colorResult = ColorHex.Create(colorHex);
        if(colorResult.IsFailed)
            errors.AddRange(colorResult.Errors);

        if (errors.Any())
            return Result.Fail<Project>(errors);

        var project = new Project(
            nameResult.Value,
            notesResult.Value,
            dueDate == null ? null : dueDateResult!.Value,
            creationTimeResult.Value,
            lastModifiedTimeResult.Value,
            colorResult.Value,
            userIdResult.Value);

        return project;
    }
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
        var nameResult = ProjectName.Create(name);
        if (nameResult.IsFailed)
            return Result.Fail(nameResult.Errors);
        Name = nameResult.Value;
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
        => IsCompleted = !IsCompleted;
    public Result DeleteTask(Task task)
    {
        var taskResult = GetTask(task.Id);

        if (taskResult.IsFailed)
            return Result.Fail(taskResult.Errors);

        _tasks.Remove(taskResult.Value);
        return Result.Ok();
    }
    public Result AddTask(Task task)
    {
        var alreadyExist = _tasks.Any(t => t == task);
        if (alreadyExist)
        {
            return Result.Fail(
                new TaskAlreadyExistsError(task.Id));
        }
        _tasks.Add(task);
        return Result.Ok();
    }
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

        if (tagResult.IsFailed)
            return Result.Fail(tagResult.Errors);

        _tags.Remove(tagResult.Value);
        return Result.Ok();
    }
    private Result<Tag> GetTag(TagId id)
    {
        var tag = _tags.SingleOrDefault(t => t.Id == id);

        return tag ?? Result.Fail<Tag>(
            new NotFoundError(nameof(Tag), id));
    }
    private Result<Task> GetTask(TaskId id)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        return task ?? Result.Fail<Task>(
            new NotFoundError(nameof(Task), id));
    }
}