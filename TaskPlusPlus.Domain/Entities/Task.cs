using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Task : Entity
{
    private readonly List<Tag> _tags = new();
    public override TaskId Id { get; }
    public TaskName Name { get; set; }
    public DueDate DueDate { get; set; }
    public Notes Notes { get; set; }
    public CreationTime CreationTime { get; set; }
    public LastModifiedTime LastModifiedTime { get; set; }
    public bool IsCompleted { get; set; }
    public TimeOnly DurationTime { get; set; }
    public Priority Priority { get; set; }
    public Energy Energy { get; set; }
    public ProjectId? ProjectId { get; set; }
    //TODO: Add User id when will be created
    //TODO: Think about Category
    public IReadOnlyCollection<Tag> Tags => _tags;

    public Task(
        TaskName name,
        DueDate dueDate,
        Notes notes,
        DateTime lastModifiedTime, 
        Priority priority, 
        ProjectId? projectId, Energy energy)
    {
        Id = new TaskId(new Guid());
        Name = name;
        DueDate = dueDate;
        Notes = notes;
        LastModifiedTime = lastModifiedTime;
        Priority = priority;
        ProjectId = projectId;
        Energy = energy;
        CreationTime = DateTime.Now;
        IsCompleted = false;
    }
} 