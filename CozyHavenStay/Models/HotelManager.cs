using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class HotelManager
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Foreign key reference
        public Hotel? Hotel { get; set; }

        public User? User { get; set; }
    }
}
