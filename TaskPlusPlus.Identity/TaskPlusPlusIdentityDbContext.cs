using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Identity.Configurations;
using TaskPlusPlus.Identity.Models;

namespace TaskPlusPlus.Identity;
public class TaskPlusPlusIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public TaskPlusPlusIdentityDbContext(DbContextOptions<TaskPlusPlusIdentityDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    }
}
