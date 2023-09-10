using FluentResults;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Tag;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Task : Entity
{
    private readonly List<Tag> _tags = new();
    private readonly List<Task> _subTasks = new();
    public override TaskId Id { get; }
    internal TaskName Name { get; private set; }
    private DueDate? _dueDate;
    private Notes _notes;
    private CreationTime _creationTime;  
    private LastModifiedTime _lastModifiedTime;
    private bool _isCompleted;
    private TimeOnly? _durationTime;
    private Priority _priority;
    private Energy _energy;
    private ProjectId? _projectId;

    //TODO: Add User id when will be created
    //TODO: Think about Category

    private Task(
        TaskName name,
        DueDate? dueDate,
        Notes notes,
        LastModifiedTime lastModifiedTime, 
        Priority priority, 
        ProjectId? projectId, 
        Energy energy,
        TimeOnly? durationTime,
        CreationTime creationTime)
    {
        Id = new TaskId(new Guid());
        Name = name;
        _dueDate = dueDate;
        _notes = notes;
        _lastModifiedTime = lastModifiedTime;
        _priority = priority;
        _projectId = projectId;
        _energy = energy;
        _durationTime = durationTime;
        _creationTime = creationTime;
        _isCompleted = false;
    }

    public Result<Task> Create(
        string name,
        DateTime dueDate,
        string notes,
        Priority priority,
        ProjectId? projectId,
        Energy energy,
        TimeOnly durationTime)
    {
        var errors = new List<IError>();

        var creationTimeResult = CreationTime.Create(DateTime.Now);
        if (creationTimeResult.IsFailed)
            errors.AddRange(creationTimeResult.Errors);

        var lastModifiedTimeResult = LastModifiedTime.Create(creationTimeResult.Value);
        if (lastModifiedTimeResult.IsFailed)
            errors.AddRange(lastModifiedTimeResult.Errors);

        var nameResult = TaskName.Create(name);
        if (nameResult.IsFailed)
            errors.AddRange(nameResult.Errors);

        var dueDateResult = DueDate.Create(dueDate);
        if (dueDateResult.IsFailed)
            errors.AddRange(dueDateResult.Errors);

        var notesResult = Notes.Create(notes);
        if (notesResult.IsFailed)
            errors.AddRange(notesResult.Errors);

        if (errors.Any())
            return Result.Fail<Task>(errors);

        var task = new Task(nameResult.Value,
            dueDateResult.Value,
            notesResult.Value,
            lastModifiedTimeResult.Value,
            priority,
            projectId,
            energy,
            durationTime,
            creationTimeResult.Value);

        return task;
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

        if(tagResult.IsFailed)
            return Result.Fail(tagResult.Errors);

        _tags.Remove(tagResult.Value);
        return Result.Ok();
    }
    public Result DeleteSubTask(Task task)
    {
        var taskResult = GetTask(task.Id);

        if (taskResult.IsFailed)
            return Result.Fail(taskResult.Errors);

        _subTasks.Remove(taskResult.Value);
        return Result.Ok();
    }
    public Result AddSubTask(Task task)
    {
        var alreadyExist = _subTasks.Any(t => t == task);
        if (alreadyExist)       
        {
            return Result.Fail(
                new TaskAlreadyExistsError(task.Id));
        }
        _subTasks.Add(task);
        return Result.Ok();
    }
    public void ChangePriority(Priority priority)
    => _priority = priority;
    public Result UpdateDueDate(DateTime dueDate)
    {
        var dueDateResult = DueDate.Create(dueDate);
        if (dueDateResult.IsFailed)
            return Result.Fail(dueDateResult.Errors);
        _dueDate = dueDateResult.Value;
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
        _notes = notesResult.Value;
        return Result.Ok();
    }
    public void ChangeCompleteState()
    => _isCompleted = !_isCompleted;
    private Result<Tag> GetTag(TagId id)
    {
        var tag = _tags.SingleOrDefault(t => t.Id == id);

        return tag ?? Result.Fail<Tag>(
            new ItemNotFoundError(typeof(Tag), tag!.Name));
    }
    private Result<Task> GetTask(TaskId id)
    {
        var task = _subTasks.SingleOrDefault(t => t.Id == id);

        return task ?? Result.Fail<Task>(
            new ItemNotFoundError(typeof(Task), task!.Name));
    }
} 