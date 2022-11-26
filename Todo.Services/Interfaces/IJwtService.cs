using Todo.Core.Models;
using Todo.Shared.Requests.Auth;

namespace Todo.Services.Interfaces;

public interface IJwtService
{
    Task<string> GenerateAccessTokenAsync(User user);
    Task<string> GenerateRefreshToken(User user);

    Task<string> RefreshAccessTokenAsync(RefreshTokenRequest refreshTokenDto);
}