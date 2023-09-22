using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

public sealed class Project : Entity<ProjectId>, IAuditEntity
{
    public ProjectName Name { get; private set; } = null!;
    public Notes Notes { get; private set; } = null!;
    public DueDate? DueDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public IReadOnlyCollection<Task> Tasks => _tasks;
    public UserId UserId { get; private set; } = null!;
    public CategoryId CategoryId { get; private set; }

    private readonly List<Task> _tasks = new();

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public DateTime? CompletedOnUtc { get; private set; }

    private Project()
    {

    }
    public Project(
        ProjectName name,
        Notes notes, 
        DueDate? dueDate,
        UserId userId, 
        CategoryId categoryId)
    {
        Name = name;
        Notes = notes;
        DueDate = dueDate;
        UserId = userId;
        CategoryId = categoryId;
        IsCompleted = false;
    }

    public static Result<Project> Create(
        string name,
        string notes,
        DateTime? dueDate,
        string userId,
        CategoryId categoryId)
    {

        var errors = new List<IError>();

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


        if (errors.Any())
            return Result.Fail<Project>(errors);

        var project = new Project(
            nameResult.Value,
            notesResult.Value,
            dueDate == null ? null : dueDateResult!.Value,
            userIdResult.Value,
            categoryId);

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
    
    private Result<Task> GetTask(TaskId id)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        return task ?? Result.Fail<Task>(
            new NotFoundError(nameof(Task), id));
    }
}