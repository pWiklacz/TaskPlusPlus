using Microsoft.EntityFrameworkCore;
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
        var dueDateConverter = new ValueConverter<DueDate, DateTime>(
            dueDate => dueDate.Value,
            value => DueDate.Create(value).Value);
        var categoryIdConverter = new ValueConverter<CategoryId, ulong>(
            categoryId => categoryId.Value,
            value => new CategoryId(value));
        var projectIdConverter = new ValueConverter<ProjectId, ulong>(
            projectId => projectId.Value,
            value => new ProjectId(value));
        var priorityConverter = new ValueConverter<Priority, string>(
            priority => priority.Name,
            value => Priority.FromName(value).Value!);
        var energyConverter = new ValueConverter<Energy, string>(
            energy => energy.Name,
            value => Energy.FromName(value).Value!);
        var durationTimeConverter = new ValueConverter<TimeOnly, TimeSpan>(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(t => t.Id)
            .HasConversion(idConverter);

        builder.Property(t => t.Name)
            .HasConversion(nameConverter);

        builder.Property(t => t.UserId)
            .HasConversion(userIdConverter);

        builder.Property(t => t.Notes)
            .HasConversion(notesConverter);

        builder.Property(t => t.DueDate)
            .HasConversion(dueDateConverter!);

        builder.Property(t => t.CategoryId)
            .HasConversion(categoryIdConverter);

        builder.Property(t => t.ProjectId)
            .HasConversion(projectIdConverter);

        builder.Property(t => t.Priority)
            .HasConversion(priorityConverter);

        builder.Property(t => t.Energy)
            .HasConversion(energyConverter);

        builder.Property(t => t.DurationTime)
            .HasConversion(durationTimeConverter);

        builder.HasMany(t => t.Tags)
            .WithMany();

    }
}
