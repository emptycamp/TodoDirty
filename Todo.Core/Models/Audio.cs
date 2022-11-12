using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Core.Models
{
    public class Audio: EntityBase
    {
        public required string Path { get; set; }

        [ForeignKey(nameof(User))]
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
