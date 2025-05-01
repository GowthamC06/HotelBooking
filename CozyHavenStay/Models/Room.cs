using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string Type { get; set; } = "Standard"; 

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; } 

        [Required]
        [Range(0, 100000)]
        public decimal PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string Amenities { get; set; } = string.Empty; // "AC, WiFi, TV, Mini-bar"

        public string Description { get; set; } = string.Empty;

        [Url]
        [MaxLength(500)]
        public string Url { get; set; } = string.Empty; // NEW: Room image URL

        // Foreign key to Hotel
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        // Navigation property
        public Hotel? Hotel { get; set; }

        // Related bookings
        public ICollection<Booking>? Bookings { get; set; }

    }
}
