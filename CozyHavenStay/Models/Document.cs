namespace CozyHavenStay.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty; // e.g., "ID Proof"
        public byte[] Data { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
