using AutoMapper;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Infrastructure.Exceptions;
using Todo.Services.Exceptions;
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

        public async Task<ICollection<AudioResponse>> GetAllEntities()
        {
            var audios = await _audioRepository.GetAll();
            var audiosDto = _mapper.Map<ICollection<AudioResponse>>(audios);
            return audiosDto;
        }

        public async Task<AudioResponse?> GetEntity(int id)
        {
            var audio = await _audioRepository.FindById(id);
            var audioDto = _mapper.Map<AudioResponse>(audio);
            return audioDto;
        }

        public async Task<AudioResponse> CreateEntity(CreateAudioRequest entityDto)
        {
            try
            {
                var audio = _mapper.Map<Audio>(entityDto);
                var createdAudio = await _noteRepository.AddAudio(entityDto.NoteId, audio);
                var createdAudioDto = _mapper.Map<AudioResponse>(createdAudio);
                return createdAudioDto;
            }
            catch (RepositoryDoesntExistException exception)
            {
                throw new ServiceException(nameof(entityDto.NoteId), exception.Message);
            }
        }

        public async Task<AudioResponse> UpdateEntity(int id, CreateAudioRequest entityDto)
        {
            var audio = _mapper.Map<Audio>(entityDto);
            audio.Id = id;

            var updatedAudio = await _audioRepository.Update(audio);
            var updatedAudioDto = _mapper.Map<AudioResponse>(updatedAudio);
            return updatedAudioDto;
        }

        public async Task DeleteEntity(int id)
        {
            await _audioRepository.Delete(id);
        }
    }
}
