using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; } 

        [Range(1, 5)]
        public int StarRating { get; set; } 

        public string Amenities { get; set; } = string.Empty; // Gym,Pool etc...

        [Url]
        [MaxLength(500)]
        public string Url { get; set; } = string.Empty; // NEW: Hotel image URL
        public int HotelManagerId { get; set; }
        public HotelManager? HotelManager { get; set; }


        // Navigation properties

        public ICollection<Room> Rooms { get; set; } = new List<Room>(); //  Associated rooms

        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // User feedback
    }
}
