namespace CozyHavenStay.Models.DTOs
{
    public class CreateRefundRequest
    {
        public int PaymentId { get; set; }
        public decimal AmountRefunded { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
