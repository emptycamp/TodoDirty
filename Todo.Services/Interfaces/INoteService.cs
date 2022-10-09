using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Interfaces;

public interface INoteService: ICrudService<CreateNoteRequest, NoteResponse>
{
}