using AutoMapper;
using Todo.Core.Models;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Mappers
{
    public class DocumentMapper: Profile
    {
        public DocumentMapper()
        {
            CreateMap<CreateDocumentRequest, Document>();
            CreateMap<CreateNoteRequest, Note>();
            CreateMap<CreateAudioRequest, Audio>();

            CreateMap<Document, DocumentResponse>();
            CreateMap<Note, NoteResponse>();
            CreateMap<Audio, AudioResponse>();
        }
    }
}
