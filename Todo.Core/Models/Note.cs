namespace Todo.Core.Models
{
    public class Note: EntityBase
    {
        public required string Title { get; set; }
        public string? Text { get; set; }

        public required List<Audio> Audios { get; set; }
    }
}