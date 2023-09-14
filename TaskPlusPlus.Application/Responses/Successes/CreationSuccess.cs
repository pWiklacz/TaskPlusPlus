using FluentResults;

namespace TaskPlusPlus.Application.Responses.Successes;

internal class CreationSuccess : Success
{
    public CreationSuccess(string type)
    : base($"{type} created successfully")
    {
    }
}