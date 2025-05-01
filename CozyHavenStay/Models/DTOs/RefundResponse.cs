namespace CozyHavenStay.Models.DTOs
{
    public class RefundResponse
    {
        public int RefundId { get; set; }
        public int PaymentId { get; set; }
        public decimal AmountRefunded { get; set; }
        public DateTime RefundDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
