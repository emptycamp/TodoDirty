using Todo.Core.Exceptions;
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
    }

    private static async Task HandleNotFoundException(HttpContext context, Exception e)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        await context.Response.WriteAsJsonAsync(new ErrorResponse(e.Message));
    }
}