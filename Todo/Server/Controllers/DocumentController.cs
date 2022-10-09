using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Shared.Requests;
using Todo.Shared.Responses;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<DocumentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocuments()
    {
        return Ok(await _documentService.GetAllEntities());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocument(int id)
    {
        return Ok(await _documentService.GetEntity(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDocument(CreateDocumentRequest document)
    {
        var createdDocument = await _documentService.CreateEntity(document);
        return CreatedAtAction("GetDocument", new { id = createdDocument.Id }, createdDocument);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateDocument(int id, CreateDocumentRequest document)
    {
        var createdDocument = await _documentService.UpdateEntity(id, document);
        return Ok(createdDocument);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        await _documentService.DeleteEntity(id);
        return Ok();
    }
}