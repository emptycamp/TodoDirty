﻿using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Infrastructure.Exceptions;
using Todo.Services.Exceptions;
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

    public async Task<ICollection<NoteResponse>> GetAllEntities()
    {
        var notes = await _noteRepository.GetAll();
        var notesDto = _mapper.Map<ICollection<NoteResponse>>(notes);
        return notesDto;
    }

    public async Task<NoteResponse?> GetEntity(int id)
    {
        var note = await _noteRepository.FindById(id);
        var noteDto = _mapper.Map<NoteResponse>(note);
        return noteDto;
    }

    public async Task<NoteResponse> CreateEntity(CreateNoteRequest entityDto)
    {
        try
        {
            var note = _mapper.Map<Note>(entityDto);
            var createdNote = await _documentRepository.AddNote(entityDto.DocumentId, note);
            var createdNoteDto = _mapper.Map<NoteResponse>(createdNote);
            return createdNoteDto;
        }
        catch (RepositoryDoesNotExistException exception)
        {
            throw new ServiceException(nameof(entityDto.DocumentId), exception.Message);
        }

    }

    public async Task<NoteResponse> UpdateEntity(int id, CreateNoteRequest entityDto)
    {
        var note = _mapper.Map<Note>(entityDto);
        note.Id = id;

        var updatedNote = await _noteRepository.Update(note);
        var updatedNoteDto = _mapper.Map<NoteResponse>(updatedNote);
        return updatedNoteDto;
    }

    public async Task DeleteEntity(int id)
    {
        await _noteRepository.Delete(id);
    }
}