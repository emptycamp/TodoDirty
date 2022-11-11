using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure;

namespace Todo.Server.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection SetupSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
