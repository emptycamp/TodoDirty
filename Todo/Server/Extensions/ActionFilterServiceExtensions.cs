using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Todo.Server.Filters;

namespace Todo.Server.Extensions
{
    public static class ActionFilterServiceExtensions
    {
        public static IServiceCollection SetupActionFilters(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
                options.Filters.Add<ErrorActionFilterAttribute>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
