using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateDocumentRequest
    {
        /// <summary>
        /// Document's title
        /// </summary>
        /// <example>My new document!</example>
        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string Title { get; set; } = null!;
    }
}
