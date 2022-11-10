namespace Todo.Shared.Responses
{
    public class DocumentResponse
    {
        /// <summary>
        /// Document ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Document Title
        /// </summary>
        /// <example>Nutrition for gaining weight</example>>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Document notes
        /// </summary>
        public List<NoteResponse> Notes { get; set; } = new();
    }
}