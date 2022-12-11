using Todo.Core.Models;

namespace Todo.Shared.Responses
{
    public record AudioResponse
    {
        /// <summary>
        /// Audio ID
        /// </summary>
        /// <example>1</example>
        public required int Id { get; set; }

        /// <summary>
        /// Path to the audio resource
        /// </summary>
        /// <example>/audios/c0342f19-32ec-4bdf-88d1-a5f20e89e9c4.mp3</example>
        public required string Path { get; set; }

        public UserResponse User { get; set; }
    }
}