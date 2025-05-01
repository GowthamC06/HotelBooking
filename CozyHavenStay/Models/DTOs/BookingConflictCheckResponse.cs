namespace CozyHavenStay.Models.DTOs
{
    public class BookingConflictCheckResponse
    {
        public bool HasConflict { get; set; }
        public string Message { get; set; } = "No conflicting bookings";
    }
}
