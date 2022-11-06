namespace Todo.Core.Models
{
    public class Note: EntityBase
    {
        public string Title { get; set; } = null!;
        public string? Text { get; set; }

        public List<Audio> Audios { get; set; } = new();
    }
}