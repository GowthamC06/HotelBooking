using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Models
{
    public class Refund
    {
        [Key]
        public int RefundId { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        public decimal AmountRefunded { get; set; }

        public DateTime RefundDate { get; set; } = DateTime.UtcNow;

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "Refunded"; // Pending, Rejected, Refunded

        public Payment? Payment { get; set; }
    }
}