using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Models
{
    public class Document
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = null!;

        public List<Note> Notes { get; set; } = new();
    }
}