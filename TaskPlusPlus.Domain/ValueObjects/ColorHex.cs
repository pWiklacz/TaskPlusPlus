using FluentResults;
using System.Text.RegularExpressions;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects.Task;

namespace TaskPlusPlus.Domain.ValueObjects;

public sealed class ColorHex : ValueObject
{
    public string Value { get; }

    public ColorHex(string value)
    {
        Value = value;
    }

    public static Result<ColorHex> Create(string colorHex)
    {
        if (!IsValidHexColor(colorHex))
        {
            return Result.Fail<ColorHex>(
                new InvalidHexColorFormatError(colorHex));
        }

        return new ColorHex(colorHex);
    }

    private static bool IsValidHexColor(string colorHex)
    {
        const string hexPattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
        return Regex.IsMatch(colorHex, hexPattern);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}