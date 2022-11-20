using Microsoft.AspNetCore.Identity;
using Todo.Core.Models;
using Todo.Infrastructure.Seeds;

namespace Todo.Server.Extensions
{
    public static class SeedingApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabase(this WebApplication webApplication)
        {
            await using var scope = webApplication.Services.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            await UserSeeder.SeedRoles(roleManager);
            await UserSeeder.SeedUsers(userManager);

            return webApplication;
        } 
    }
}
