using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastructure
{
    public class AudioRepository: RepositoryBaseWithUser<Audio>, IAudioRepository
    {
        public AudioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
