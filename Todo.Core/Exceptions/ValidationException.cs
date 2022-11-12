namespace Todo.Core.Exceptions;

public class ValidationException: Exception
{
    public IEnumerable<FieldError> Errors { get; }

    public ValidationException(IEnumerable<FieldError> errors)
    {
        Errors = errors;
    }
}

public class FieldError
{
    public required string Field { get; set; }
    public required IEnumerable<string> Messages { get; set; }
}