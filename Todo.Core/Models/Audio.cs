using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Models
{
    public class Audio
    {
        public int Id { get; set; }
        [Required] public string Path { get; set; } = null!;
    }
}