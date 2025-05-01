namespace CozyHavenStay.Models.DTOs
{
    public class PaymentStatsResponse
    {
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public Dictionary<string, int> PaymentMethodCount { get; set; } = new();
    }
}
