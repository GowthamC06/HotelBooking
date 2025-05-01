namespace CozyHavenStay.Models.DTOs
{
    public class RoomFilter
    {
        public string? Type { get; set; }
        public bool? IsAvailable { get; set; }
        public PriceRange? PriceRange { get; set; }
        public int? MinCapacity { get; set; }
    }
}
