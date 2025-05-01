namespace CozyHavenStay.Models.DTOs
{
    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string Amenities { get; set; }
        public string Description { get; set; }
        public string Url { get; set; } = string.Empty;
        public int HotelId { get; set; }
        public string Message { get; set; } = "Room details retrieved successfully";
    }

}
