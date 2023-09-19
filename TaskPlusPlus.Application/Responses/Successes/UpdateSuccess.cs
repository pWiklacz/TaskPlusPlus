using FluentResults;

namespace TaskPlusPlus.Application.Responses.Successes;
internal class UpdateSuccess : Success
{
    public UpdateSuccess(string type)
        : base($"{type} updated successfully")
    {
    }
}
