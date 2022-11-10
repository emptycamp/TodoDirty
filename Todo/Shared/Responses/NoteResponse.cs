namespace Todo.Shared.Responses
{
    public class NoteResponse
    {
        /// <summary>
        /// Note ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Note title
        /// </summary>
        /// <example>Meal preps with high calorie density</example>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Note text contents
        /// </summary>
        /// <example>...contains over 50g of proteins and...</example>
        public string? Text { get; set; }

        /// <summary>
        /// Audio recordings that belong to the note
        /// </summary>
        public List<AudioResponse> Audios { get; set; } = new();
    }
}