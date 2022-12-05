using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Shared.Requests.Auth;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[AllowAnonymous]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(CreateUserRequest userDto)
    {
        var user = await _authService.CreateUserAsync(userDto);
        // TODO# add Location header
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUser(AuthenticateUserRequest userDto)
    {
        var user = await _authService.AuthenticateUserAsync(userDto);
        return Ok(user);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshToken)
    {
        var accessTokenResponse = await _authService.RefreshToken(refreshToken);
        return Ok(accessTokenResponse);
    }
}