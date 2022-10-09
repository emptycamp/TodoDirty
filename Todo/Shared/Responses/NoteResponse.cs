using Todo.Core.Models;

namespace Todo.Shared.Responses
{
    public class NoteResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Text { get; set; }
        public List<Audio> Audios { get; set; } = new();
    }
}