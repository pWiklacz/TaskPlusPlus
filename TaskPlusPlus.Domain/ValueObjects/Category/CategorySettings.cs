using FluentResults;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Category;
public sealed class CategorySettings
{
    public string Grouping { get; set; } = "None";
    public string Sorting { get; set; } = "Name";
    public bool Direction { get; set; } = false;
}
