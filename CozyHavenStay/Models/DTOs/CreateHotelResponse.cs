namespace CozyHavenStay.Models.DTOs
{
    public class CreateHotelResponse
    {
        public int HotelId { get; set; }
        public string Message { get; set; } = "Hotel created successfully";
    }
}
