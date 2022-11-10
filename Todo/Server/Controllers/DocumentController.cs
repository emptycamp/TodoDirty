using Microsoft.AspNetCore.Mvc;
using Todo.Services.Interfaces;
using Todo.Shared.Queries;
using Todo.Shared.Requests;
using Todo.Shared.Responses;
using Todo.Shared.Responses.Errors;

namespace Todo.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    /// <summary>
    /// Gets documents
    /// </summary>
    /// <response code="200">Successfully retrieved documents</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DocumentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocuments([FromQuery] LimitQuery limitQuery)
    {
        return Ok(await _documentService.GetAllEntities(limitQuery.Limit, limitQuery.Offset));
    }

    /// <summary>
    /// Gets document by ID with all of its notes
    /// </summary>
    /// <param name="limitQuery">Pagination in query</param>
    /// <param name="id">Document ID</param>
    /// <response code="200">Successfully retrieved document</response>
    /// <response code="404">Document not found</response>
    [HttpGet("{id}/notes")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocuments([FromQuery] LimitQuery limitQuery, int id)
    {
        return Ok(await _documentService.GetDocumentWithNotes(id, limitQuery.Limit, limitQuery.Offset));
    }

    /// <summary>
    /// Gets document by ID with selected note and all of its audios
    /// </summary>
    /// <param name="limitQuery">Pagination in query</param>
    /// <param name="documentId">Document ID</param>
    /// <param name="noteId">note ID</param>
    /// <response code="200">Successfully retrieved document</response>
    /// <response code="404">Document not found</response>
    [HttpGet("{documentId}/notes/{noteId}/audios")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocuments([FromQuery] LimitQuery limitQuery, int documentId, int noteId)
    {
        return Ok(await _documentService.GetDocumentWithNoteWithAudios(documentId, noteId, limitQuery.Limit, limitQuery.Offset));
    }

    /// <summary>
    /// Gets document by ID
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <response code="200">Successfully retrieved document</response>
    /// <response code="404">Document not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocument(int id)
    {
        return Ok(await _documentService.GetEntity(id));
    }

    /// <summary>
    /// Gets document by ID with selected note and selected audio
    /// </summary>
    /// <param name="documentId">Document ID</param>
    /// <param name="noteId">Note ID</param>
    /// <param name="audioId">Audio ID</param>
    /// <response code="200">Successfully retrieved document</response>
    /// <response code="404">Document not found</response>
    [HttpGet("{documentId}/notes/{noteId}/audios/{audioId}")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocument(int documentId, int noteId, int audioId)
    {
        return Ok(await _documentService.GetDocumentWithNoteWithAudio(documentId, noteId, audioId));
    }

    /// <summary>
    /// Create document
    /// </summary>
    /// <param name="document">Document object</param>
    /// <response code="201">Created document successfully</response>
    /// <response code="422">Invalid document specified</response>
    [HttpPost]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateDocument(CreateDocumentRequest document)
    {
        var createdDocument = await _documentService.CreateEntity(document);
        return CreatedAtAction("GetDocument", new { id = createdDocument.Id }, createdDocument);
    }

    /// <summary>
    /// Updates document
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <param name="document">Document Object</param>
    /// <response code="200">Successfully updated document</response>
    /// <response code="404">Document not found</response>
    /// <response code="422">Invalid document specified</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateDocument(int id, CreateDocumentRequest document)
    {
        var createdDocument = await _documentService.UpdateEntity(id, document);
        return Ok(createdDocument);
    }

    /// <summary>
    /// Deletes document
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <response code="200">Successfully deleted document</response>
    /// <response code="404">Document not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        await _documentService.DeleteEntity(id);
        return Ok();
    }
}