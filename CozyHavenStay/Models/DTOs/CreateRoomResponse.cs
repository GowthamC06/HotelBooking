namespace CozyHavenStay.Models.DTOs
{
    public class CreateRoomResponse
    {
        public int RoomId { get; set; }
        public string Message { get; set; } = "Room created successfully";
    }
}
