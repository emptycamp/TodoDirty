using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Server.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services
{
    public class AudioService : IAudioService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IAudioRepository _audioRepository;
        private readonly IMapper _mapper;

        public AudioService(INoteRepository noteRepository, IAudioRepository audioRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _audioRepository = audioRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<AudioResponse>> GetAllEntities(int limit, int offset)
        {
            var audios = await _audioRepository.GetAll(limit, offset);
            var audiosDto = _mapper.Map<ICollection<AudioResponse>>(audios);
            return audiosDto;
        }

        public async Task<AudioResponse?> GetEntity(int id)
        {
            var audio = await _audioRepository.FindByIdOrThrow(id);
            var audioDto = _mapper.Map<AudioResponse>(audio);
            return audioDto;
        }

        public async Task<AudioResponse> CreateEntity(CreateAudioRequest entityDto, Guid userId)
        {
            var audio = _mapper.Map<Audio>(entityDto);

            var noteEntity = await _noteRepository.FindByIdOrThrow(entityDto.NoteId);
            if (noteEntity.UserId != userId)
            {
                throw new UnauthorizedException("Note does not belong to current user");
            }

            audio.Id = noteEntity.Id;
            audio.UserId = noteEntity.UserId;

            var createdAudio = await _noteRepository.AddAudio(entityDto.NoteId, audio);
            var createdAudioDto = _mapper.Map<AudioResponse>(createdAudio);
            return createdAudioDto;
        }

        public async Task<AudioResponse> CreateImpersonatedEntity(CreateAudioRequest entityDto)
        {
            var noteEntity = await _noteRepository.FindByIdOrThrow(entityDto.NoteId);

            var audio = _mapper.Map<Audio>(entityDto);
            audio.UserId = noteEntity.UserId;

            var createdAudio = await _noteRepository.AddAudio(entityDto.NoteId, audio);
            var createdAudioDto = _mapper.Map<AudioResponse>(createdAudio);
            return createdAudioDto;
        }

        public async Task<AudioResponse> UpdateEntity(int id, CreateAudioRequest entityDto, Guid? userId)
        {
            var audio = _mapper.Map<Audio>(entityDto);
            audio.Id = id;

            var audioEntity = await _audioRepository.FindByIdOrThrow(id);
            if (userId != null && audioEntity.UserId != userId)
            {
                throw new UnauthorizedException("Audio does not belong to current user");
            }

            var updatedAudio = await _audioRepository.Update(audio);
            var updatedAudioDto = _mapper.Map<AudioResponse>(updatedAudio);
            return updatedAudioDto;
        }

        public async Task DeleteEntity(int id, Guid? userId)
        {
            var audioEntity = await _audioRepository.FindByIdOrThrow(id);
            if (userId != null && audioEntity.UserId != userId)
            {
                throw new UnauthorizedException("Audio does not belong to current user");
            }

            await _audioRepository.Delete(audioEntity);
        }
    }
}
