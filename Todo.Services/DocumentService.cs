using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    private readonly IMapper _mapper;

    public DocumentService(IDocumentRepository documentRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<DocumentResponse>> GetAllEntities()
    {
        var documents = await _documentRepository.GetAll();
        var documentsDto = _mapper.Map<ICollection<DocumentResponse>>(documents);
        return documentsDto;
    }

    public async Task<DocumentResponse?> GetEntity(int id)
    {
        var document = await _documentRepository.FindById(id);
        var documentDto = _mapper.Map<DocumentResponse>(document);
        return documentDto;
    }

    public async Task<DocumentResponse> CreateEntity(CreateDocumentRequest entityDto)
    {
        var document = _mapper.Map<Document>(entityDto);
        var createdDocument = await _documentRepository.Create(document);
        var createdDocumentDto = _mapper.Map<DocumentResponse>(createdDocument);
        return createdDocumentDto;
    }

    public async Task<DocumentResponse> UpdateEntity(int id, CreateDocumentRequest entityDto)
    {
        var document = _mapper.Map<Document>(entityDto);
        document.Id = id;

        var updatedDocument = await _documentRepository.Update(document);
        var updatedDocumentDto = _mapper.Map<DocumentResponse>(updatedDocument);
        return updatedDocumentDto;
    }

    public async Task DeleteEntity(int id)
    {
        await _documentRepository.Delete(id);
    }
}