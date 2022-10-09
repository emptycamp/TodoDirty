using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateNoteRequest
    {
        [Required]
        public int DocumentId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Text { get; set; }
    }
}