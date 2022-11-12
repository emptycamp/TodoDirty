using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Server.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services;

public class NoteService : INoteService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public NoteService(IDocumentRepository documentRepository, INoteRepository noteRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _noteRepository = noteRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<NoteResponse>> GetAllEntities(int limit, int offset)
    {
        var notes = await _noteRepository.GetAll(limit, offset);
        var notesDto = _mapper.Map<ICollection<NoteResponse>>(notes);
        return notesDto;
    }

    public async Task<NoteResponse?> GetEntity(int id)
    {
        var note = await _noteRepository.FindByIdOrThrow(id);
        var noteDto = _mapper.Map<NoteResponse>(note);
        return noteDto;
    }

    public async Task<NoteResponse> CreateEntity(CreateNoteRequest entityDto, Guid userId)
    {
        var note = _mapper.Map<Note>(entityDto);
        note.UserId = userId;

        var documentEntity = await _documentRepository.FindByIdOrThrow(entityDto.DocumentId);
        if (documentEntity.UserId != userId)
        {
            throw new UnauthorizedException("Document does not belong to current user");
        }

        var createdNote = await _documentRepository.AddNote(entityDto.DocumentId, note);
        var createdNoteDto = _mapper.Map<NoteResponse>(createdNote);
        return createdNoteDto;
    }

    public async Task<NoteResponse> CreateImpersonatedEntity(CreateNoteRequest entityDto)
    {
        var documentEntity = await _documentRepository.FindByIdOrThrow(entityDto.DocumentId);

        var note = _mapper.Map<Note>(entityDto);
        note.UserId = documentEntity.UserId;

        var createdNote = await _documentRepository.AddNote(entityDto.DocumentId, note);
        var createdNoteDto = _mapper.Map<NoteResponse>(createdNote);
        return createdNoteDto;
    }

    public async Task<NoteResponse> UpdateEntity(int id, CreateNoteRequest entityDto, Guid? userId)
    {
        var note = _mapper.Map<Note>(entityDto);
        note.Id = id;

        var noteEntity = await _noteRepository.FindByIdOrThrow(id);
        if (userId != null && noteEntity.UserId != userId)
        {
            throw new UnauthorizedException("Note does not belong to current user");
        }

        var updatedNote = await _noteRepository.Update(note);
        var updatedNoteDto = _mapper.Map<NoteResponse>(updatedNote);
        return updatedNoteDto;
    }

    public async Task DeleteEntity(int id, Guid? userId)
    {
        var noteEntity = await _noteRepository.FindByIdOrThrow(id);
        if (userId != null && noteEntity.UserId != userId)
        {
            throw new UnauthorizedException("Note does not belong to current user");
        }

        await _noteRepository.Delete(noteEntity);
    }
}