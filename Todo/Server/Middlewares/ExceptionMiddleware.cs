using AutoMapper;
using Todo.Core.Exceptions;
using Todo.Server.Exceptions;
using Todo.Services.Exceptions;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Middlewares;

public class ExceptionMiddleware: IMiddleware
{
    private readonly IMapper _mapper;

    public ExceptionMiddleware(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleNotFoundException(context, e);
        }
        catch (ValidationException e)
        {
            await HandleValidationException(context, e);
        }
        catch (Exception e) when (e is AuthenticationException or UnauthorizedException)
        {
            await HandleAuthenticationException(context, e);
        }
    }

    private async Task HandleValidationException(HttpContext context, ValidationException e)
    {
        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        var response = _mapper.Map<ValidationErrorResponse>(e);
        await context.Response.WriteAsJsonAsync(response);
    }

    private static async Task HandleNotFoundException(HttpContext context, Exception e)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        await context.Response.WriteAsJsonAsync(new ErrorResponse(e.Message));
    }

    private static async Task HandleAuthenticationException(HttpContext context, Exception e)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await context.Response.WriteAsJsonAsync(new ErrorResponse(e.Message));
    }
}