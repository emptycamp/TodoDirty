using Microsoft.EntityFrameworkCore;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastructure
{
    public class NoteRepository: RepositoryBaseWithUser<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Audio> AddAudio(int id, Audio audio)
        {
            var note = await FindByIdOrThrowTracked(id);
            note.Audios.Add(audio);
            await Context.SaveChangesAsync();

            return audio;
        }

        public override async Task<ICollection<Note>> GetAll(int limit = 1000, int offset = 0)
        {
            return await Table.Skip(offset).Take(limit).Include(x => x.User).Include(x => x.Audios).ToListAsync();
        }
    }
}
