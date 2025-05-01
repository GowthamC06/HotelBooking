namespace CozyHavenStay.Models.DTOs
{
    public class CancelBookingResponse
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = "Cancelled";
        public string Message { get; set; } = "Booking has been cancelled successfully";
    }
}
