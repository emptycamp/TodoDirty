using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        public Task<Note> AddNote(int documentId, Note note);
        public Task<Document> GetDocumentWithNotesOrThrow(int documentId, int limit = 1000, int offset = 0);
        public Task<Document> GetDocumentWithNoteWithAudiosOrThrow(int documentId, int noteId, int limit = 1000,
            int offset = 0);
        public Task<Document> GetDocumentWithNoteWithAudioOrThrow(int documentId, int noteId, int audioId);

    }
}
