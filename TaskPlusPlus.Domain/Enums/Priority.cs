using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Enums;

public sealed class Priority : Enumeration<Priority>
{
    public byte SubLevel { get; }
    public static Priority A(byte subLevel) => new(5, nameof(A), subLevel);
    public static Priority B(byte subLevel) => new(4, nameof(B), subLevel);
    public static Priority C(byte subLevel) => new(3, nameof(C), subLevel);
    public static Priority D(byte subLevel) => new(2, nameof(D), subLevel);
    public static Priority E(byte subLevel) => new(1, nameof(E), subLevel);
    private Priority(int value, string name, byte subLevel = 0) 
        : base(value, name)
    {
        SubLevel = subLevel;
    }
    public override string ToString()
    {
        return SubLevel is 0
            ? Name 
            : Name + SubLevel;
    }
}