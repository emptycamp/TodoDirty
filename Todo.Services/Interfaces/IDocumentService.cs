using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Interfaces;

public interface IDocumentService : ICrudService<CreateDocumentRequest, DocumentResponse>
{
    public Task<DocumentResponse> GetDocumentWithNotes(int documentId, int limit, int offset);
    public Task<DocumentResponse> GetDocumentWithNoteWithAudios(int documentId, int noteId, int limit, int offset);
    public Task<DocumentResponse> GetDocumentWithNoteWithAudio(int documentId, int noteId, int audioId);
}
