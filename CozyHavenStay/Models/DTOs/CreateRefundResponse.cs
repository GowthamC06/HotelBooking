namespace CozyHavenStay.Models.DTOs
{
    public class CreateRefundResponse
    {
        public int RefundId { get; set; }
        public string Message { get; set; } = "Refund issued successfully";
    }
}
