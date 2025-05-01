namespace CozyHavenStay.Models.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
    }
}
