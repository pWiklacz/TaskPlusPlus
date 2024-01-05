using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskPlusPlus.Domain.Enums;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects.Project;
using TaskPlusPlus.Domain.ValueObjects.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Persistence.Configurations;
public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        var nameConverter = new ValueConverter<TaskName, string>(
            name => name.Value,
            value => TaskName.Create(value).Value);
        var userIdConverter = new ValueConverter<UserId, string>(
            userId => userId.Value,
            value => UserId.Create(value).Value);
        var idConverter = new ValueConverter<TaskId, ulong>(
            taskId => taskId.Value,
            value => new TaskId(value));

        var notesConverter = new ValueConverter<Notes, string>(
            notes => notes.Value,
            value => Notes.Create(value).Value);

        var categoryIdConverter = new ValueConverter<CategoryId, ulong>(
            categoryId => categoryId.Value,
            value => new CategoryId(value));
        var projectIdConverter = new ValueConverter<ProjectId?, ulong?>(
            projectId => projectId,
            value => value.HasValue ? new ProjectId(value.Value) : null);
        var priorityConverter = new ValueConverter<Priority, int>(
            priority => priority.Value,
            value => Priority.FromValue(value).Value!);
        var energyConverter = new ValueConverter<Energy, int>(
            energy => energy.Value,
            value => Energy.FromValue(value).Value!);

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

        builder.HasKey(t => t.Id);

        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(t => t.Id)
            .HasConversion(idConverter)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .HasConversion(nameConverter);

        builder.Property(t => t.UserId)
            .HasConversion(userIdConverter);

        builder.Property(t => t.Notes)
            .HasConversion(notesConverter);

        builder.Property(t => t.DueDate)
            .HasConversion(dueDateConverter!, dueDateComparer);

        builder.Property(t => t.CategoryId)
            .HasConversion(categoryIdConverter);

        builder.Property(t => t.ProjectId)
            .HasConversion(projectIdConverter)
            .IsRequired(false);

        builder.Property(t => t.Priority)
            .HasConversion(priorityConverter);

        builder.Property(t => t.Energy)
            .HasConversion(energyConverter);

        builder.Property(t => t.DueTime)
            .HasConversion(dueTimeConverter, dueTimeComparer);

        builder.HasMany(t => t.Tags)
            .WithMany();

    }
}
