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
public class NoteController : ApiControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<NoteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotes([FromQuery] LimitQuery limitQuery)
    {
        return Ok(await _noteService.GetAllEntities(limitQuery.Limit, limitQuery.Offset));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNote(int id)
    {
        return Ok(await _noteService.GetEntity(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNote(CreateNoteRequest note)
    {
        NoteResponse createdDocument;

        if (IsAdmin)
        {
            createdDocument = await _noteService.CreateImpersonatedEntity(note);
        }
        else
        {
            createdDocument = await _noteService.CreateEntity(note, UserId);
        }

        return CreatedAtAction("GetNote", new { id = createdDocument.Id }, createdDocument);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNote(int id, CreateNoteRequest note)
    {
        var createdNote = await _noteService.UpdateEntity(id, note, IsAdmin ? null : UserId);
        return Ok(createdNote);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteNote(int id)
    {
        await _noteService.DeleteEntity(id, IsAdmin ? null : UserId);
        return Ok();
    }
}