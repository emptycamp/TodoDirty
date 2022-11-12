using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Services.Interfaces;

public interface IAudioService: ICrudService<CreateAudioRequest, AudioResponse>, ICreateImpersonate<CreateAudioRequest, AudioResponse>
{
}