using Microsoft.AspNetCore.Identity;
using Todo.Shared.Requests;
using Todo.Shared.Requests.Auth;
using Todo.Shared.Responses.Auth;

namespace Todo.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> CreateUserAsync(CreateUserRequest userDto);
    Task<AccessTokenResponse> AuthenticateUserAsync(AuthenticateUserRequest authenticateUserDto);
    Task<AccessTokenResponse> RefreshToken(RefreshTokenRequest refreshToken);
}