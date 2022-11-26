using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Server.Exceptions;
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

    public async Task<ICollection<DocumentResponse>> GetAllEntities(int limit, int offset)
    {
        var documents = await _documentRepository.GetAll(limit, offset);
        var documentsDto = _mapper.Map<ICollection<DocumentResponse>>(documents);
        return documentsDto;
    }

    public async Task<DocumentResponse?> GetEntity(int id)
    {
        var document = await _documentRepository.FindByIdOrThrow(id);
        var documentDto = _mapper.Map<DocumentResponse>(document);
        return documentDto;
    }

    public async Task<DocumentResponse> GetDocumentWithNotes(int documentId, int limit, int offset)
    {
        var document = await _documentRepository.GetDocumentWithNotesOrThrow(documentId, limit, offset);
        var documentDto = _mapper.Map<DocumentResponse>(document);
        return documentDto;
    }
    public async Task<DocumentResponse> GetDocumentWithNoteWithAudios(int documentId, int noteId, int limit, int offset)
    {
        var document = await _documentRepository.GetDocumentWithNoteWithAudiosOrThrow(documentId, noteId, limit, offset);
        var documentDto = _mapper.Map<DocumentResponse>(document);
        return documentDto;
    }

    public async Task<DocumentResponse> GetDocumentWithNoteWithAudio(int documentId, int noteId, int audioId)
    {
        var document = await _documentRepository.GetDocumentWithNoteWithAudioOrThrow(documentId, noteId, audioId);
        var documentDto = _mapper.Map<DocumentResponse>(document);
        return documentDto;
    }

    public async Task<DocumentResponse> CreateEntity(CreateDocumentRequest entityDto, Guid userId)
    {
        var document = _mapper.Map<Document>(entityDto);
        document.UserId = userId;

        var createdDocument = await _documentRepository.Create(document);
        var createdDocumentDto = _mapper.Map<DocumentResponse>(createdDocument);
        return createdDocumentDto;
    }

    public async Task<DocumentResponse> UpdateEntity(int id, CreateDocumentRequest entityDto, Guid? userId)
    {
        var document = _mapper.Map<Document>(entityDto);

        var documentEntity = await _documentRepository.FindByIdOrThrow(id);
        if (userId != null && documentEntity.UserId != userId)
        {
            throw new UnauthorizedException("Document does not belong to current user");
        }

        document.Id = id;
        document.UserId = documentEntity.UserId;

        var updatedDocument = await _documentRepository.Update(document);
        var updatedDocumentDto = _mapper.Map<DocumentResponse>(updatedDocument);
        return updatedDocumentDto;
    }

    public async Task DeleteEntity(int id, Guid? userId)
    {
        // TODO# cascade on delete
        var documentEntity = await _documentRepository.FindByIdOrThrow(id);
        if (userId != null && documentEntity.UserId != userId)
        {
            throw new UnauthorizedException("Document does not belong to current user");
        }

        await _documentRepository.Delete(documentEntity);
    }
}