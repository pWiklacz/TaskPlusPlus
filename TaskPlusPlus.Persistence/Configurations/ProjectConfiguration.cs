using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Persistence.Configurations;
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        var nameConverter = new ValueConverter<ProjectName, string>(
              name => name.Value,
            value => ProjectName.Create(value).Value);
        var userIdConverter = new ValueConverter<UserId, string>(
            userId => userId.Value,
            value => UserId.Create(value).Value);
        var idConverter = new ValueConverter<ProjectId, ulong>(
            projectId => projectId.Value,
            value => new ProjectId(value));
        var notesConverter = new ValueConverter<Notes, string>(
            notes => notes.Value,
            value => Notes.Create(value).Value);
        var dueDateConverter = new ValueConverter<DueDate, DateTime>(
            dueDate => dueDate.Value,
            value => DueDate.Create(value).Value);
        var categoryIdConverter = new ValueConverter<CategoryId, ulong>(
            categoryId => categoryId.Value,
            value => new CategoryId(value));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(p => p.Id)
            .HasConversion(idConverter);

        builder.Property(p => p.Name)
            .HasConversion(nameConverter);

        builder.Property(p => p.UserId)
            .HasConversion(userIdConverter);

        builder.Property(p => p.Notes)
            .HasConversion(notesConverter);

        builder.Property(p => p.DueDate)
            .HasConversion(dueDateConverter!);

        builder.Property(p => p.CategoryId)
            .HasConversion(categoryIdConverter);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }   
}
 