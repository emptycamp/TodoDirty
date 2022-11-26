using Todo.Core.Interfaces;
using Todo.Infrastructure;
using Todo.Server.Middlewares;
using Todo.Services.Interfaces;
using Todo.Services;
using Todo.Shared.Store;

namespace Todo.Server.Extensions
{
    public static class InjectionServiceExtensions
    {
        public static IServiceCollection SetupInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ExceptionMiddleware>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteService, NoteService>();

            services.AddScoped<IAudioRepository, AudioRepository>();
            services.AddScoped<IAudioService, AudioService>();

            services.AddScoped<ITokenRepository, TokenRepository>();

            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            return services;
        }
    }
}
