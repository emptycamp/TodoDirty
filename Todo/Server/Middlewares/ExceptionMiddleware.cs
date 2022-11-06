using Todo.Core.Exceptions;
using Todo.Services.Exceptions;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Middlewares;

public class ExceptionMiddleware: IMiddleware
{
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
        catch (AuthenticationException e)
        {
            await HandleAuthenticationException(context, e);
        }
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