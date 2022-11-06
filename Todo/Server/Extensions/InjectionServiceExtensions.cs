using Todo.Core.Interfaces;
using Todo.Infrastructure;
using Todo.Server.Middlewares;
using Todo.Services.Interfaces;
using Todo.Services;

namespace Todo.Server.Extensions
{
    public static class InjectionServiceExtensions
    {
        public static IServiceCollection SetupInjections(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteService, NoteService>();

            services.AddScoped<IAudioRepository, AudioRepository>();
            services.AddScoped<IAudioService, AudioService>();

            return services;
        }
    }
}
