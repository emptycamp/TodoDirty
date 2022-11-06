using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Todo.Core.Models;
using Todo.Infrastructure;

namespace Todo.Server.Extensions
{
    public static class BuilderAuthenticationExtensions
    {
        public static WebApplicationBuilder SetupAuthentication(this WebApplicationBuilder builder)
        {
            AddIdentity(builder);
            AddAuthentication(builder);

            return builder;
        }

        private static void AddAuthentication(WebApplicationBuilder builder)
        {
            var jwtConfig = builder.Configuration.GetSection("JwtConfig");

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = jwtConfig["ValidIssuer"],
                        ValidAudience = jwtConfig["ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"]))
                    };
                });
        }

        private static void AddIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
