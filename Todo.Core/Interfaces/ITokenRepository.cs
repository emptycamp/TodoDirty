using Todo.Core.Models;

namespace Todo.Core.Interfaces;

public interface ITokenRepository
{
    Task<RefreshToken> FindByRefreshToken(string refreshToken);
    Task AddRefreshToken(RefreshToken refreshToken);
}