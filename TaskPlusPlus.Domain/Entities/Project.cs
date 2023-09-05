using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Project : Entity
{
    public Project(Guid id) : base(id)
    {
    }
}