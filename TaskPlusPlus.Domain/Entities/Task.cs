using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Task : Entity
{
    public TaskName Name { get; set; }
    public DueDate DueDate { get; set; }
    public TaskNotes Notes { get; set; }
    public CreationTime CreationTime { get; set; }
    public LastModifiedTime LastModifiedTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsCyclical { get; set; } = false;

    public Task(Guid id, TaskName name, DueDate dueDate, TaskNotes notes, DateTime lastModifiedTime) : base(id)
    {
        Name = name;
        DueDate = dueDate;
        Notes = notes;
        LastModifiedTime = lastModifiedTime;
        CreationTime = DateTime.Now;
    }
} 