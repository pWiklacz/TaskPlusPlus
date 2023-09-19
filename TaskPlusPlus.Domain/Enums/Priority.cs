using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Enums;

public sealed class Priority : Enumeration<Priority>
{
    public static Priority A => new(5, nameof(A));
    public static Priority B => new(4, nameof(B));
    public static Priority C => new(3, nameof(C));
    public static Priority D => new(2, nameof(D));
    public static Priority E => new(1, nameof(E));
    private Priority(int value, string name) 
        : base(value, name)
    {
    }
    public override string ToString()
    {
        return Name;
    }
}