using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class User : Entity
{
    public User(Guid id) : base(id)
    {
    }

}