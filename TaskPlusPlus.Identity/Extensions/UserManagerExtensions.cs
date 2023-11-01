using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskPlusPlus.Identity.Models;

namespace TaskPlusPlus.Identity.Extensions;

public static class UserManagerExtensions
{
     public static async Task<ApplicationUser?> FindByEmailFromClaimsPrincipal(this UserManager<ApplicationUser> userManager, 
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
}