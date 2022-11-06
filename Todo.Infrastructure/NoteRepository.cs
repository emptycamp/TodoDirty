using Microsoft.EntityFrameworkCore;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Infrastructure.Exceptions;

namespace Todo.Infrastructure
{
    public class NoteRepository: RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Audio> AddAudio(int id, Audio audio)
        {
            var note = await FindById(id);

            if (note == null)
            {
                throw new RepositoryDoesNotExistException("Note doesn't exist");
            }

            note.Audios.Add(audio);
            await Context.SaveChangesAsync();

            return audio;
        }

        public override async Task<ICollection<Note>> GetAll(int limit = 1000)
        {
            return await Table.Take(limit).Include(x => x.Audios).ToListAsync();
        }
    }
}
