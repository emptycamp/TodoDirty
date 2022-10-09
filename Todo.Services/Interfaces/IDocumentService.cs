using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Interfaces;

public interface IDocumentService: ICrudService<CreateDocumentRequest, DocumentResponse>
{
}