using Microsoft.AspNetCore.Identity;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Constants;

namespace SponsorSphereWebAPI.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task CreateAdmin(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var admin = await userManager.FindByEmailAsync(UserConstants.AdminUserEmail);

            if (admin is not null)
            {
                var isAdmin = await userManager.IsInRoleAsync(admin, RoleConstants.Admin);
                
                if (!isAdmin)
                {
                    await userManager.AddToRoleAsync(admin, RoleConstants.Admin);
                }
            }
        }
    }
}
