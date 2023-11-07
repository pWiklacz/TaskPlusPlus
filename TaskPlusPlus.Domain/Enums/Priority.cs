using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Enums;

public sealed class Priority : Enumeration<Priority>
{
    public static readonly Priority A = new(5, nameof(A));
    public static readonly Priority B = new(4, nameof(B));
    public static readonly Priority C = new(3, nameof(C));
    public static readonly Priority D = new(2, nameof(D));
    public static readonly Priority E = new(1, nameof(E));
    private Priority(int value, string name) 
        : base(value, name)
    {
    }
    public override string ToString()
    {
        return Name;
    }
}