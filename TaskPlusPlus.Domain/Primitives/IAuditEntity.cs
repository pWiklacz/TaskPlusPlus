namespace TaskPlusPlus.Domain.Primitives;

public interface IAuditEntity
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
    
}