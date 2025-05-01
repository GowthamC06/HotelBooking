using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using AutoMapper;

namespace CozyHavenStay.Misc
{
    public class HotelMapper : Profile
    {
        public HotelMapper()
        {
            CreateMap<Hotel, HotelDTO>();
            CreateMap<Hotel, HotelResponse>();
            CreateMap<CreateHotelRequest, Hotel>();
        }
    }
}
