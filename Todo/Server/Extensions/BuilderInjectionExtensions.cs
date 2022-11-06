using Todo.Core.Interfaces;
using Todo.Infrastructure;
using Todo.Server.Middlewares;
using Todo.Services.Interfaces;
using Todo.Services;

namespace Todo.Server.Extensions
{
    public static class BuilderInjectionExtensions
    {
        public static WebApplicationBuilder SetupInjections(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ExceptionMiddleware>();

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            builder.Services.AddScoped<INoteRepository, NoteRepository>();
            builder.Services.AddScoped<INoteService, NoteService>();

            builder.Services.AddScoped<IAudioRepository, AudioRepository>();
            builder.Services.AddScoped<IAudioService, AudioService>();

            return builder;
        }
    }
}
