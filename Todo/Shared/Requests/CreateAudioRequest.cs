using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateAudioRequest
    {
        [Required]
        public int NoteId { get; set; }
        [Required]
        public string Path { get; set; } = null!;
    }
}