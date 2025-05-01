namespace CozyHavenStay.Models.DTOs
{
    public class PaymentFilterRequest
    {
        public string? Method { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
