namespace CozyHavenStay.Models.DTOs
{
    public class ReviewResponse
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string Message { get; set; } = "Review retrieved successfully";
    }
}
