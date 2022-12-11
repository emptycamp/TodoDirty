using Todo.Core.Models;

namespace Todo.Shared.Responses
{
    public record DocumentResponse
    {
        /// <summary>
        /// Document ID
        /// </summary>
        /// <example>1</example>
        public required int Id { get; set; }

        /// <summary>
        /// Document Title
        /// </summary>
        /// <example>Nutrition for gaining weight</example>>
        public required string Title { get; set; }

        /// <summary>
        /// Document notes
        /// </summary>
        public required List<NoteResponse> Notes { get; set; }
        public required UserResponse User { get; set; }
    }
}