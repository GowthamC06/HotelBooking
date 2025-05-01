namespace CozyHavenStay.Models.DTOs
{
    public class BookingRequest
    {
        public BookingFilter? Filters { get; set; }
        public int? SortBy { get; set; } // 1: CheckIn ASC, -1: CheckIn DESC
        public Pagination? Pagination { get; set; }
    }
}
