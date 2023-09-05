using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Task : Entity
{
    public TaskName Name { get; set; }

    public Task(Guid id, TaskName name) : base(id)
    {
        Name = name;
    }

}