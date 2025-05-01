using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string PaymentMethod { get; set; } = "Card"; // Card, UPI, NetBanking, Cash

        public string Status { get; set; } = "Success"; // Pending, Failed, Refunded

        public string? TransactionReference { get; set; }

        // Navigation
        public Booking? Booking { get; set; }
    }
}
