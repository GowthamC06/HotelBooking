using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int Rating { get; set; } // ⭐ Rating out of 5

        public string Comment { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign Key: Hotel
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }

        // Foreign Key: Customer
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}

