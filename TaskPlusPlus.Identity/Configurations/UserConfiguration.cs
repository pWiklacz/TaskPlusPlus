using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskPlusPlus.Application.Constants;
using TaskPlusPlus.Identity.Models;

namespace TaskPlusPlus.Identity.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var admin = new ApplicationUser
        {
            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            FirstName = "System",
            LastName = "Admin",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
        };
        admin.PasswordHash = hasher.HashPassword(admin, "P@ssword1");
        admin.EmailConfirmed = true;

        var user = new ApplicationUser
            {
                Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
            };
        user.PasswordHash = hasher.HashPassword(user, "P@ssword1");
        user.EmailConfirmed = true;

        builder.HasData(
            admin,
            user);
    }
}
