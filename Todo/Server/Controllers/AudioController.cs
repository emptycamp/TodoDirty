using Microsoft.AspNetCore.Mvc;
using Todo.Services.Exceptions;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;


namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AudioController : ControllerBase
{
    private readonly IAudioService _audioService;

    public AudioController(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<AudioResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAudios()
    {
        return Ok(await _audioService.GetAllEntities());
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
        try
        {
            var createdDocument = await _audioService.CreateEntity(audio);
            return CreatedAtAction("GetAudio", new { id = createdDocument.Id }, createdDocument);
        }
        catch (ServiceException e)
        {
            ModelState.AddModelError(e.ParamName, e.Message);
        }

        return ValidationProblem(ModelState);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AudioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAudio(int id, CreateAudioRequest audio)
    {
        var createdAudio = await _audioService.UpdateEntity(id, audio);
        return Ok(createdAudio);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAudio(int id)
    {
        await _audioService.DeleteEntity(id);
        return Ok();
    }
}