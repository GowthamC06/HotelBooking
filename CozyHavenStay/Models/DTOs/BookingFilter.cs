namespace CozyHavenStay.Models.DTOs
{
    public class BookingFilter
    {
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? CustomerId { get; set; }
        public int? HotelId { get; set; }
    }
}
