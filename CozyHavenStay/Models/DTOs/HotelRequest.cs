namespace CozyHavenStay.Models.DTOs
{
    public class HotelRequest
    {
        public HotelFilter? Filters { get; set; }
        //public int? SortBy { get; set; }  // e.g., 1: Name ASC, -1: Name DESC, 2: Rating ASC, -2: Rating DESC
        //public Pagination? Pagination { get; set; }
    }
}
