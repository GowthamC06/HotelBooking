namespace CozyHavenStay.Models.DTOs
{
    public class CreateBookingResponse
    {
        public int BookingId { get; set; }
        public string Message { get; set; } = "Booking confirmed";
    }
}
