using Microsoft.EntityFrameworkCore;
using Todo.Core;
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
                throw new RepositoryDoesntExistException("Note doesn't exist");
            }

            note.Audios.Add(audio);
            await _context.SaveChangesAsync();

            return audio;
        }

        public override async Task<ICollection<Note>> GetAll(int limit = 1000)
        {
            return await _table.Take(limit).Include(x => x.Audios).ToListAsync();
        }
    }
}
