namespace CozyHavenStay.Models.DTOs
{
    public class CreateHotelRequest
    {
        public string HotelName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public int StarRating { get; set; }
        public string Amenities { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int HotelManagerId { get; set; }
    }
}
