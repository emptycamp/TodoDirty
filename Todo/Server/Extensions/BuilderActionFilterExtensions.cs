using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Todo.Server.Validations;

namespace Todo.Server.Extensions
{
    public static class BuilderActionFilterExtensions
    {
        public static WebApplicationBuilder SetupActionFilters(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter());


                options.Filters.Add<ErrorActionFilterAttribute>();
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return builder;
        }
    }
}
