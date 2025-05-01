namespace CozyHavenStay.Models.DTOs
{
    public class CreateCustomerResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = "Customer created successfully";
    }
}
