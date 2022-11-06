using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure;

namespace Todo.Server.Extensions
{
    public static class BuilderDatabaseExtensions
    {
        public static WebApplicationBuilder SetupSqlServer(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return builder;
        }
    }
}
