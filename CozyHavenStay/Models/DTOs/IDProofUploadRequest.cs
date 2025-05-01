namespace CozyHavenStay.Models.DTOs
{
    public class IDProofUploadRequest
    {
        public int CustomerId { get; set; }
        public IFormFile IDProof { get; set; } = null!;
    }
}
