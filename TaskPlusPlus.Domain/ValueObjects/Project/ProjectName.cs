using FluentResults;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.Primitives;

namespace TaskPlusPlus.Domain.ValueObjects.Project;

public sealed class ProjectName : ValueObject
{
    public const ushort MaxLength = 500;
    private ProjectName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ProjectName> Create(string projectName)
    {
        if (string.IsNullOrEmpty(projectName))
        {
            return Result.Fail<ProjectName>(
                new EmptyStringError(nameof(ProjectName)));
        }

        if (projectName.Length > MaxLength)
        {
            return Result.Fail<ProjectName>(
                new StringTooLongError(MaxLength, nameof(ProjectName)));
        }

        return new ProjectName(projectName);
    }

    public static implicit operator string(ProjectName name)
        => name.Value;

    public static implicit operator ProjectName(string name)
        => Create(name).Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}