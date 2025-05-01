namespace CozyHavenStay.Models.DTOs
{
    public class HotelResponse
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public string Amenities { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Message { get; set; } = "Hotel details retrieved successfully";
    }
}
