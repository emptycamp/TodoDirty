using Todo.Core.Models;

namespace Todo.Shared.Responses
{
    public class DocumentResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<Note> Notes { get; set; } = new();
    }
}