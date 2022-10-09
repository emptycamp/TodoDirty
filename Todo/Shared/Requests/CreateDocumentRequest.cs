using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateDocumentRequest
    {
        [Required]
        public string Title { get; set; } = null!;
    }
}
