namespace CozyHavenStay.Models.DTOs
{
    public class CreatePaymentRequest
    {
        public int BookingId { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? TransactionReference { get; set; }
    }
}
