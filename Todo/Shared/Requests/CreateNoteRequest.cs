namespace Todo.Shared.Requests
{
    public record CreateNoteRequest
    {
        public required int DocumentId { get; set; }
        public required string Title { get; set; }
        public string? Text { get; set; }
    }
}