namespace Todo.Core.Models
{
    public class Document: EntityBase
    {
        public required string Title { get; set; }

        public required List<Note> Notes { get; set; } 
    }
}