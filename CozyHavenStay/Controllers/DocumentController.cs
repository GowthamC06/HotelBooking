using Microsoft.AspNetCore.Mvc;
using CozyHavenStay.Models;
using CozyHavenStay.Services;
using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Interfaces;

namespace CozyHavenStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // Endpoint to upload ID proof document for a customer
        [HttpPost("upload/idproof/{customerId}")]
        public async Task<IActionResult> UploadIdProofDocument(int customerId, [FromForm] IDProofUploadRequest request)
        {
            try
            {
                await _documentService.SaveIdProofDocumentsAsync(customerId, request);
                return Ok("ID Proof document uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading ID Proof document: {ex.Message}");
            }
        }

        // Endpoint to download the ID proof document for a customer
        [HttpGet("download/idproof/{customerId}")]
        public async Task<IActionResult> DownloadIdProofDocument(int customerId)
        {
            try
            {
                var document = await _documentService.DownloadIdProofDocumentAsync(customerId);
                if (document.HasValue)
                {
                    return File(document.Value.FileData, "application/octet-stream", document.Value.FileName);
                }
                else
                {
                    return NotFound("ID Proof document not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading ID Proof document: {ex.Message}");
            }
        }
    }
}
