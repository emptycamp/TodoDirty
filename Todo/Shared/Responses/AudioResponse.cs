using Todo.Core.Models;

namespace Todo.Shared.Responses
{
    public class AudioResponse
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
    }
}