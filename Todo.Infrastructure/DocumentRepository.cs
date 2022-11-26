using Microsoft.EntityFrameworkCore;
using Todo.Core.Exceptions;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Infrastructure
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Note> AddNote(int documentId, Note note)
        {
            var document = await FindByIdOrThrowTracked(documentId);
            document.Notes.Add(note);
            await Context.SaveChangesAsync();

            return note;
        }

        public async Task<Document> GetDocumentWithNotesOrThrow(int documentId, int limit = 1000, int offset = 0)
        {
            return await Table
                .Include(x => x.Notes.Skip(offset).Take(limit))
                .FirstOrDefaultAsync(x => x.Id == documentId)
                   ?? throw new NotFoundException(nameof(Document));
        }

        public async Task<Document> GetDocumentWithNoteWithAudiosOrThrow(int documentId, int noteId, int limit = 1000, int offset = 0)
        {
            var result = await Table
                       .Include(x => x.Notes)
                       .ThenInclude(x => x.Audios.Skip(offset).Take(limit))
                       .FirstOrDefaultAsync(doc => doc.Id == documentId)
                   ?? throw new NotFoundException(nameof(Document));

            result.Notes = result.Notes.Where(x => x.Id == noteId).ToList();

            if (!result.Notes.Any())
            {
                throw new NotFoundException(nameof(Note));
            }

            return result;
        }

        public async Task<Document> GetDocumentWithNoteWithAudioOrThrow(int documentId, int noteId, int audioId)
        {
            var result = await Table
                .Include(doc => doc.Notes)
                .ThenInclude(note => note.Audios)
                .Where(doc => doc.Id == documentId)
                .Select(doc => new
                {
                    Document = doc,
                    Note = doc.Notes.FirstOrDefault(note => note.Id == noteId)
                })
                .Select(doc => new
                {
                    doc.Document,
                    doc.Note,
                    Audios = doc.Note == null ? null : doc.Note.Audios.FirstOrDefault(audio => audio.Id == audioId)
                }).FirstOrDefaultAsync();

            if (result == null)
            {
                throw new NotFoundException("Document");
            }

            if (result.Note == null)
            {
                throw new NotFoundException("Note");
            }

            if (result.Audios == null)
            {
                throw new NotFoundException("Audios");
            }

            var document = result.Document;

            result.Note.Audios = new List<Audio> { result.Audios };
            document.Notes = new List<Note> { result.Note };

            return document;
        }
    }
}
