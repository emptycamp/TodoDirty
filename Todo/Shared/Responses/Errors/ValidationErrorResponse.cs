namespace Todo.Shared.Responses.Errors
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public ValidationErrorResponse(IEnumerable<FieldError> errors) :
            base("Validation failed", errors)
        {
        }
    }
}
