namespace CozyHavenStay.Models.DTOs
{
    public class BookingConflictCheckRequest
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
