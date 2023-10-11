using FluentResults;

namespace TaskPlusPlus.Domain.Errors;
public class BaseError : Error
{
    public ushort Code { get; }
    public BaseError(ushort code, string message)
        : base(message)
    {
        Code = code;
    }
}
