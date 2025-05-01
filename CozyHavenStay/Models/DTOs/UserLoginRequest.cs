using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Models.DTOs
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
