using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Shared.Queries;
using Todo.Shared.Requests;
using Todo.Shared.Responses;
using Todo.Shared.Responses.Errors;


namespace Todo.Server.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
public class AudioController : ApiControllerBase
{
    private readonly IAudioService _audioService;

    public AudioController(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AudioResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudios([FromQuery] LimitQuery limitQuery)
    {
        return Ok(await _audioService.GetAllEntities(limitQuery.Limit, limitQuery.Offset));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AudioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudio(int id)
    {
        return Ok(await _audioService.GetEntity(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AudioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAudio(CreateAudioRequest audio)
    {
        var createdDocument = await _audioService.CreateEntity(audio, UserId);
        return CreatedAtAction("GetAudio", new { id = createdDocument.Id }, createdDocument);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AudioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAudio(int id, CreateAudioRequest audio)
    {
        var createdAudio = await _audioService.UpdateEntity(id, audio, IsAdmin ? null : UserId);
        return Ok(createdAudio);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAudio(int id)
    {
        await _audioService.DeleteEntity(id, IsAdmin ? null : UserId);
        return Ok();
    }
}