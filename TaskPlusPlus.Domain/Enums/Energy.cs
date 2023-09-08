using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.Enums;

internal sealed class Energy : Enumeration<Energy>
{
    public static Energy None = new Energy(0, nameof(None));
    public static Energy Low => new Energy(1, nameof(Low));
    public static Energy Med => new Energy(2, nameof(Med));
    public static Energy High = new Energy(3, nameof(High));
    private Energy(int value, string name)
        : base(value, name)
    {
    }

    public override string ToString()
    {
        return Name;
    }
}