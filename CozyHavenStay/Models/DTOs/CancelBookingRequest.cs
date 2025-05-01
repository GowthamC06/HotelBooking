namespace CozyHavenStay.Models.DTOs
{
    public class CancelBookingRequest
    {
        public int BookingId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
