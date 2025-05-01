namespace CozyHavenStay.Models.DTOs
{
    public class CreateRoomRequest
    {
        public string RoomType { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Amenities { get; set; }
        public string Description { get; set; }
        public string Url { get; set; } = string.Empty;
        public int HotelId { get; set; }
    }
}
