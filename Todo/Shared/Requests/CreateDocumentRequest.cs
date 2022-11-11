using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateDocumentRequest
    {
        /// <summary>
        /// Document's title
        /// </summary>
        /// <example>My new document!</example>
        [StringLength(40, MinimumLength = 5)]
        public required string Title { get; set; }
    }
}
