namespace CozyHavenStay.Models.DTOs
{
    public class RoomRequest
    {
        public RoomFilter? Filters { get; set; }
        public int? SortBy { get; set; }  // 1: Price ASC, -1: Price DESC, 2: Capacity ASC, -2: DESC
        public Pagination? Pagination { get; set; }
    }
}
