using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Enums;

public sealed class Energy : Enumeration<Energy>
{
    public static Energy None = new(0, nameof(None));
    public static Energy Low => new(1, nameof(Low));
    public static Energy Med => new(2, nameof(Med));
    public static Energy High = new(3, nameof(High));
    private Energy(int value, string name)
        : base(value, name)
    {
    }
    public override string ToString()
    {
        return Name;
    }
}