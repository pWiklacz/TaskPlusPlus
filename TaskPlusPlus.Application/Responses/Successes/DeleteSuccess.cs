using FluentResults;

namespace TaskPlusPlus.Application.Responses.Successes;

internal class DeleteSuccess : Success
{
    public DeleteSuccess(string type)
        : base($"{type} deleted successfully")
    {
    }
}