using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class InvalidHexColorFormatError : Error
{
    public InvalidHexColorFormatError(string colorHex)
    :base($"{colorHex} - is invalid hexadecimal color format.")
    {
        
    }
}