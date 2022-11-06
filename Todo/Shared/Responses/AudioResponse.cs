namespace Todo.Shared.Responses
{
    public class AudioResponse
    {
        /// <summary>
        /// Audio ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Path to the audio resource
        /// </summary>
        /// <example>/audios/c0342f19-32ec-4bdf-88d1-a5f20e89e9c4.mp3</example>
        public string Path { get; set; } = null!;
    }
}