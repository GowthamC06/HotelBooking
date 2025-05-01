using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Interfaces
{
    public interface IDocumentService
    {
        Task SaveIdProofDocumentsAsync(int customerId, IDProofUploadRequest request);

        Task<(byte[] FileData, string FileName)?> DownloadIdProofDocumentAsync(int customerId);
    }
}
