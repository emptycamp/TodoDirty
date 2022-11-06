namespace Todo.Core.Models
{
    public class Document: EntityBase
    {
        public string Title { get; set; } = null!;

        public List<Note> Notes { get; set; } = new();
    }
}