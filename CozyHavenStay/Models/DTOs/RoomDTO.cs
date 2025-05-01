namespace CozyHavenStay.Models.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
    }
}
