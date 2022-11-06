using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Validations
{
    public class ErrorActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var fieldErrors = context.ModelState.Keys.Select(key =>
                    new FieldError
                    {
                        Field = key,
                        Messages = context.ModelState[key]!.Errors.Select(err => err.ErrorMessage)
                    }).Where(x => x.Messages.Any());

                var errorResponse = new ValidationErrorResponse(fieldErrors);

                context.Result = new UnprocessableEntityObjectResult(errorResponse);
            }
        }
    } 
}
