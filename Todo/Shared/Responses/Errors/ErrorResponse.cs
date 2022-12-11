using Todo.Core.Exceptions;

namespace Todo.Shared.Responses.Errors;

public class ErrorResponse
{
    public string Reason { get; }
    public string TraceId { get; set; } = Guid.NewGuid().ToString();
    public IEnumerable<FieldError>? Errors { get; }

    public ErrorResponse(string reason, IEnumerable<FieldError>? errors = null)
    {
        Reason = reason;
        Errors = errors;
    }
    public string FirstErrorMessage => Errors?.FirstOrDefault()?.Messages.FirstOrDefault() ?? Reason;
}