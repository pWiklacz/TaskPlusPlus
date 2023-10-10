using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Persistence.Extensions;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Persistence;

public class TaskPlusPlusDbContext : DbContext
{
    private readonly IMediator _mediator;
    public TaskPlusPlusDbContext(DbContextOptions<TaskPlusPlusDbContext> options, IMediator mediator)
    : base(options)
    {
        _mediator = mediator;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("application");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskPlusPlusDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _mediator.DispatchDomainEventsAsync(this);
        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}