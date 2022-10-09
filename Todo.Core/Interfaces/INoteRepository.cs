using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        public Task<Audio> AddAudio(int id, Audio audio);
    }
}
