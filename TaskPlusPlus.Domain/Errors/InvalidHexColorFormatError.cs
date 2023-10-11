using FluentResults;

namespace TaskPlusPlus.Domain.Errors;

public class InvalidHexColorFormatError : BaseError
{
    public InvalidHexColorFormatError(string colorHex)
    :base(400, $"{colorHex} - is invalid hexadecimal color format.")
    {
        
    }
}