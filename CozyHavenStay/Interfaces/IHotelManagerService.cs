using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
namespace CozyHavenStay.Interfaces
{
    public interface IHotelManagerService
    {
        Task<CreateHotelManagerResponse> CreateHotelManager(CreateHotelManagerRequest request);
        Task<IEnumerable<HotelManager>> GetAllHotelManagers();
        Task<HotelManager?> GetManagerById(int id);
        Task<HotelManager?> UpdateHotelManager(int id, UpdateHotelManagerRequest request);
        Task<HotelManager?> DeleteHotelManager(int id);

    }
}

