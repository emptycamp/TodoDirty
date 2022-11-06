using Todo.Core.Models;
using Todo.Infrastructure;

namespace Todo.Server.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection SetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
