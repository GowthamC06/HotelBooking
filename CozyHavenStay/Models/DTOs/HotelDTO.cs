namespace CozyHavenStay.Models.DTOs
{
    public class HotelDTO
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public string Amenities { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
