using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        var categoryIdConverter = new ValueConverter<CategoryId, ulong>(
            categoryId => categoryId.Value,
            value => new CategoryId(value));

        var dueTimeConverter = new ValueConverter<TimeOnly, TimeSpan>(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan));

        var dueTimeComparer = new ValueComparer<TimeOnly>(
            (t1, t2) => t1.Ticks == t2.Ticks,
            t => t.GetHashCode());

        var dueDateConverter = new ValueConverter<DueDate, DateTime>(
            dueDate => dueDate.Value.ToDateTime(TimeOnly.MinValue),
            value => DueDate.Create(DateOnly.FromDateTime(value)).Value);

        var dueDateComparer = new ValueComparer<DueDate>(
            (d1, d2) => d1!.Value.DayNumber == d2!.Value.DayNumber,
            d => d.GetHashCode());

        builder.HasKey(p => p.Id);

        builder.Property(p => p.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(p => p.Id)
            .HasConversion(idConverter)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasConversion(nameConverter);

        builder.Property(p => p.UserId)
            .HasConversion(userIdConverter);

        builder.Property(p => p.Notes)
            .HasConversion(notesConverter);

        builder.Property(t => t.DueDate)
            .HasConversion(dueDateConverter!, dueDateComparer);

        builder.Property(t => t.DueTime)
            .HasConversion(dueTimeConverter, dueTimeComparer);

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }   
}
 