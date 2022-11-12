using AutoMapper;
using Todo.Core.Exceptions;
using Todo.Core.Models;
using Todo.Shared.Requests;
using Todo.Shared.Responses;
using Todo.Shared.Responses.Errors;

namespace Todo.Services.Mappers
{
    public class DocumentMapper: Profile
    {
        public DocumentMapper()
        {
            // TODO: extract to seperate mappers
            CreateMap<CreateDocumentRequest, Document>();
            CreateMap<CreateNoteRequest, Note>();
            CreateMap<CreateAudioRequest, Audio>();

            CreateMap<Document, DocumentResponse>();
            CreateMap<Note, NoteResponse>();
            CreateMap<Audio, AudioResponse>();

            CreateMap<CreateUserRequest, User>();

            CreateMap<ValidationException, ValidationErrorResponse>();
        }
    }
}
