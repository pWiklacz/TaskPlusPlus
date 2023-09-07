using TaskPlusPlus.Domain.Primitives;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Domain.Entities;

internal sealed class Project : Entity
{
    public override ProjectId Id { get; }
    public ProjectName Name { get; set; }
    public Notes Notes { get; set; }
    public DueDate DueDate { get; set; }
    public CreationTime CreationTime { get; set; }
    public LastModifiedTime LastModifiedTime { get; set; }
    public bool IsCompleted { get; set; }
    public ColorHex ColorHex { get; set; }


    //TODO: Add User id when will be created
    //TODO: Think how to store icon information
    public Project(
        ProjectName name,
        Notes notes, DueDate dueDate, 
        CreationTime creationTime, 
        LastModifiedTime lastModifiedTime,
        ColorHex colorHex)
    {
        Name = name;
        Notes = notes;
        DueDate = dueDate;
        CreationTime = creationTime;
        LastModifiedTime = lastModifiedTime;
        ColorHex = colorHex;
        Id = new ProjectId(new Guid());
    }
}