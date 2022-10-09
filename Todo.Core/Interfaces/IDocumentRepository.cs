using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface IDocumentRepository: IRepository<Document>
    {
        public Task<Note> AddNote(int id, Note note);
    }
}
