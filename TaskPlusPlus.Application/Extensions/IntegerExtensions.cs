
namespace TaskPlusPlus.Application.Extensions;
public static class IntegerExtensions
{
    public static string FormatMinutes(this int minutes)
    {
        if (minutes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be non-negative.");
        }

        int hours = minutes / 60;
        int remainingMinutes = minutes % 60;

        if (hours > 0 && remainingMinutes > 0)
        {
            return $"{hours}h {remainingMinutes}min";
        }
        else if (hours > 0)
        {
            return $"{hours}h";
        }
        else
        {
            return $"{remainingMinutes}min";
        }
    }
}
