using Microsoft.EntityFrameworkCore;
using Todo.Core;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Infrastructure.Exceptions;

namespace Todo.Infrastructure
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Note> AddNote(int id, Note note)
        {
            var document = await FindById(id);

            if (document == null)
            {
                throw new RepositoryDoesntExistException("Document doesn't exist");
            }

            document.Notes.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public override async Task<ICollection<Document>> GetAll(int limit = 1000)
        {
            return await _table.Take(limit).Include(x => x.Notes).ToListAsync();
        }
    }
}
