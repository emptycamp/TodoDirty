using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Models
{
    public record Note
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = null!;
        public string? Text { get; set; }

        public List<Audio> Audios { get; set; } = new();
    }
}