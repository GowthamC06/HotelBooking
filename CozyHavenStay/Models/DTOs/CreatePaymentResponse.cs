namespace CozyHavenStay.Models.DTOs
{
    public class CreatePaymentResponse
    {
        public int PaymentId { get; set; }
        public string Message { get; set; } = "Payment processed successfully";
    }
}
