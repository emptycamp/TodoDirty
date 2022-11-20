using Microsoft.AspNetCore.Identity;
using Todo.Core;
using Todo.Core.Models;
using Todo.Utils.Extensions;

namespace Todo.Infrastructure.Seeds
{
    public static class UserSeeder
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            const string defaultPassword = "example";

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@example.com",
            };

            await CreateUser(admin, defaultPassword, new[] { RoleConstants.Admin }, userManager);
        }

        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            var roles = typeof(RoleConstants).GetStaticParamValues();
            var mappedRoles = roles.Select(role => new Role
            {
                Name = role,
                NormalizedName = role.ToUpper()
            });

            foreach (var mappedRole in mappedRoles)
            {
                await CreateRole(mappedRole, roleManager);
            }
        }

        private static async Task CreateRole(Role role, RoleManager<Role> roleManager)
        {
            if (role.Name == null)
            {
                throw new ArgumentException("Role must contain Name!");
            }

            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task CreateUser(User user, string password, UserManager<User> userManager)
        {
            await CreateUser(user, password, null, userManager);
        }

        private static async Task CreateUser(User user, string password, IEnumerable<string>? roles, UserManager<User> userManager)
        {
            if (user.Email == null)
            {
                throw new ArgumentException("User must contain Email!");
            }

            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded && roles != null)
                {
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}
