using System.ComponentModel.DataAnnotations;

namespace Todo.Shared.Requests
{
    public record CreateAudioRequest
    {
        public required int NoteId { get; set; }
        public required string Path { get; set; }
    }
}