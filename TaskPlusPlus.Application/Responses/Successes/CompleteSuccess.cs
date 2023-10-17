using FluentResults;

namespace TaskPlusPlus.Application.Responses.Successes;
internal class CompleteSuccess : Success
{
    public CompleteSuccess(string type)
        : base($"{type} completed")
    {
    }
}
