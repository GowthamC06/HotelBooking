using AutoMapper;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Misc
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<Room, CreateRoomResponse>();
            CreateMap<Room, RoomDTO>();
            CreateMap<Room, RoomResponse>();
        }
    }
}
