using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
namespace CozyHavenStay.Interfaces
{
    public interface IHotelService
    {
        Task<CreateHotelResponse> AddHotel(CreateHotelRequest request);
        Task<HotelResponse?> GetHotelById(int id);
        Task<IEnumerable<HotelDTO>> GetAllHotels();
        Task<Hotel?> DeleteHotel(int id);
        Task<Hotel?> UpdateHotel(int id, CreateHotelRequest request);
        Task<IEnumerable<HotelDTO>> GetHotelsByFilter(HotelRequest request);

    }
}

