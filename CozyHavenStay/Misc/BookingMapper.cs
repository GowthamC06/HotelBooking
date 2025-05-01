using AutoMapper;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Misc
{
    public class BookingMapper : Profile
    {
        public BookingMapper() 
        {
            CreateMap<CreateBookingRequest, Booking>();
            CreateMap<Booking, CreateBookingResponse>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<Booking, BookingResponse>();
        }
    }
}
