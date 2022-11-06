using System.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

namespace Todo.Server.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection SetupSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TODO API"
                });

                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetName().Name?.StartsWith("Todo.") ?? false);

                foreach (var assembly in assemblies)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                    Debug.WriteLine($"check: {xmlPath}");

                    if (File.Exists(xmlPath))
                    {
                        Debug.WriteLine($"exists: {xmlPath}");
                        options.IncludeXmlComments(xmlPath);
                    }
                }

                //options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });

            return services;
        }
    }
}
