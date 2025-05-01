namespace CozyHavenStay.Models.DTOs
{
    public class CreateReviewResponse
    {
        public int ReviewId { get; set; }
        public string Message { get; set; } = "Review added successfully";
    }
}
