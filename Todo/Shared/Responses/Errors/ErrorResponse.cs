namespace Todo.Shared.Responses.Errors
{
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
    }

    public class FieldError
    {
        public required string Field { get; set; }
        public required IEnumerable<string> Messages { get; set; }
    }
};
