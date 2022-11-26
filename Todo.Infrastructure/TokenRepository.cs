using Microsoft.EntityFrameworkCore;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastructure;

public class TokenRepository: ITokenRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<RefreshToken> _table;

    public TokenRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = context.RefreshTokens;
    }

    public async Task<RefreshToken> FindByRefreshToken(string refreshToken)
    {
        return await _table.Include(x => x.User).FirstAsync(x => x.Token == refreshToken);
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _table.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }
}