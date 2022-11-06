using Microsoft.AspNetCore.Identity;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> CreateUserAsync(CreateUserRequest userDto);
    Task<JwtTokenResponse> AuthenticateUserAsync(AuthenticateUserRequest authenticateUserDto);
}