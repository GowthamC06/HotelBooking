using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Interfaces
{
    public interface IRoomService
    {
        Task<CreateRoomResponse> AddRoom(CreateRoomRequest request);
        Task<RoomResponse?> GetRoomById(int id);
        Task<IEnumerable<RoomDTO>> GetRoomsByHotel(int hotelId);
        Task<Room?> UpdateRoom(int id, CreateRoomRequest request);
        Task<Room?> DeleteRoom(int id);
        Task<IEnumerable<RoomDTO>> GetRoomsByFilter(RoomRequest request);

    }
}
