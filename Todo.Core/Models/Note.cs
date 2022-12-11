using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Core.Models
{
    public class Note: EntityWithUser
    {
        public required string Title { get; set; }
        public string? Text { get; set; }
        public List<Audio> Audios { get; set; } = new();
        
        [ForeignKey(nameof(User))]
        public required Guid UserId { get; set; }
        public Document Document { get; set; }
    }
}