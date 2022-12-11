namespace Todo.Shared.Responses
{
    public record NoteResponse
    {
        /// <summary>
        /// Note ID
        /// </summary>
        /// <example>1</example>
        public required int Id { get; set; }

        /// <summary>
        /// Note title
        /// </summary>
        /// <example>Meal preps with high calorie density</example>
        public required string Title { get; set; }

        /// <summary>
        /// Note text contents
        /// </summary>
        /// <example>...contains over 50g of proteins and...</example>
        public string? Text { get; set; }

        /// <summary>
        /// Audio recordings that belong to the note
        /// </summary>
        public required List<AudioResponse> Audios { get; set; }

        public required UserResponse User { get; set; }
    }
}