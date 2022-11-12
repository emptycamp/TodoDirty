using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Core.Models
{
    public class Document : EntityBase
    {
        public required string Title { get; set; }
        public List<Note> Notes { get; set; } = new();

        [ForeignKey(nameof(User))]
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}