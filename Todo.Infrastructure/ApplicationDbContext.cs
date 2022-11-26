using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Core.Models;

namespace Todo.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<Audio> Audios => Set<Audio>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}