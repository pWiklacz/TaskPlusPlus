using FluentResults;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Category;
public sealed class CategorySettings
{
    public string Grouping { get; set; } = null!;
    public string Sorting { get; set; } = null!;
    public bool Direction { get; set; }
}
