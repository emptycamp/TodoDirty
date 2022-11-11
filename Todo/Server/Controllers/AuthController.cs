using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("/Register")]
    public async Task<IActionResult> RegisterUser(CreateUserRequest userDto)
    {
        var user = await _authService.CreateUserAsync(userDto);

        // TODO: extract
        if (!user.Succeeded)
        {
            return BadRequest(new ValidationErrorResponse(user.Errors.Select(x => new FieldError
            {
                Field = x.Code,
                Messages = new List<string> { x.Description }
            })));
        }

        return StatusCode(201);
    }

    [HttpPost("/Login")]
    public async Task<IActionResult> AuthenticateUser(AuthenticateUserRequest userDto)
    {
        var user = await _authService.AuthenticateUserAsync(userDto);
        return Ok(user);
    }
}