using TaskPlusPlus.Domain.Errors;

namespace TaskPlusPlus.Application.Responses.Errors;
internal class GroupingProblemError : BaseError
{
    public GroupingProblemError(string entityName)
        : base(400, $"Problem grouping by {entityName} - not found filed like this.")
    {
    }
}
