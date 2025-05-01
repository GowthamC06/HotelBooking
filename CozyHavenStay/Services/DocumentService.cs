using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Repositories;
using Microsoft.Extensions.Logging;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<int, Document> _documentRepository;
        private readonly ILogger<DocumentService> _logger;

        private readonly List<string> _allowedExtensions = new() { ".pdf", ".jpg", ".jpeg", ".png" };
        private const long _maxFileSize = 5 * 1024 * 1024; // 5 MB

        public DocumentService(IRepository<int, Document> documentRepository, ILogger<DocumentService> logger)
        {
            _documentRepository = documentRepository;
            _logger = logger;
        }

        // Save ID Proof documents for customers
        public async Task SaveIdProofDocumentsAsync(int customerId, IDProofUploadRequest request)
        {
            var documents = new List<Document>();
            var validationErrors = new List<string>();

            async Task AddIfValid(IFormFile file, string fileType)
            {
                if (file != null)
                {
                    var result = await TryConvertToDocumentAsync(file, fileType, customerId);
                    if (result.Item1 != null)
                        documents.Add(result.Item1);
                    else
                        validationErrors.Add($"{fileType}: {result.Item2}");
                }
            }

            // Adding the ID Proof document
            await AddIfValid(request.IDProof, "ID Proof");

            if (validationErrors.Any())
            {
                _logger.LogWarning("Upload validation failed: {Errors}", string.Join(" | ", validationErrors));
                throw new Exception("Document upload failed:\n" + string.Join("\n", validationErrors));
            }

            // Saving the documents to the repository
            foreach (var doc in documents)
            {
                await _documentRepository.Add(doc);
            }
        }

        // Helper method to validate and convert the uploaded file to a Document object
        private async Task<(Document?, string)> TryConvertToDocumentAsync(IFormFile file, string fileType, int customerId)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            // Validate file extension
            if (!_allowedExtensions.Contains(extension))
                return (null, $"Invalid file type {extension}. Only PDF, JPG, JPEG, PNG allowed.");

            // Validate file size (max 5MB)
            if (file.Length > _maxFileSize)
                return (null, $"File {file.FileName} exceeds 5MB size limit.");

            try
            {
                // Read file content into memory
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                return (new Document
                {
                    FileName = file.FileName,
                    FileType = fileType,
                    Data = ms.ToArray(),
                    CustomerId = customerId
                }, null);
            }
            catch (Exception ex)
            {
                return (null, $"Error reading file {file.FileName}: {ex.Message}");
            }
        }

        // Method to download the ID Proof document
        public async Task<(byte[] FileData, string FileName)?> DownloadIdProofDocumentAsync(int customerId)
        {
            var allDocuments = await _documentRepository.GetAll();

            // Find the ID Proof document for the specified customer
            var document = allDocuments
                .FirstOrDefault(d => d.CustomerId == customerId && d.FileType.ToLower() == "id proof");

            if (document == null)
                return null;

            return (document.Data, document.FileName);
        }
    }
}
