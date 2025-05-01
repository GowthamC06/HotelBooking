namespace CozyHavenStay.Models.DTOs
{
    public class UpdateHotelManagerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? HotelId { get; set; }
    }
}
