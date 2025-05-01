namespace CozyHavenStay.Models.DTOs
{
    public class CreateReviewRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int HotelId { get; set; }
    }
}
