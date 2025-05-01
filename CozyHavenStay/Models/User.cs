using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public string Role { get; set; } = string.Empty;

        public Admin? Admin { get; set; }
        public Customer? Customer { get; set; } // navigation
        public HotelManager? HotelManager { get; set; }
    }
}
