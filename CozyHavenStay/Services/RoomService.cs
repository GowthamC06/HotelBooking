using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<int, Room> _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRepository<int, Room> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<CreateRoomResponse> AddRoom(CreateRoomRequest request)
        {
            var room = _mapper.Map<Room>(request);
            var result = await _roomRepository.Add(room);
            if (result == null)
                throw new Exception("Room creation failed");

            return new CreateRoomResponse
            {
                RoomId = result.RoomId,
                Message = "Room created successfully"
            };
        }

        public async Task<RoomResponse?> GetRoomById(int id)
        {
            var room = await _roomRepository.GetById(id);
            return room == null ? null : _mapper.Map<RoomResponse>(room);
        }

        public async Task<IEnumerable<RoomDTO>> GetRoomsByHotel(int hotelId)
        {
            var allRooms = await _roomRepository.GetAll();
            var filtered = allRooms.Where(r => r.HotelId == hotelId);
            return _mapper.Map<IEnumerable<RoomDTO>>(filtered);
        }

        public async Task<Room?> UpdateRoom(int id, CreateRoomRequest request)
        {
            var existing = await _roomRepository.GetById(id);
            if (existing == null) return null;

            var updatedRoom = _mapper.Map<Room>(request);
            updatedRoom.RoomId = id;

            return await _roomRepository.Update(id, updatedRoom);
        }

        public async Task<Room?> DeleteRoom(int id)
        {
            return await _roomRepository.Delete(id);
        }
        public async Task<IEnumerable<RoomDTO>> GetRoomsByFilter(RoomRequest request)
        {
            var rooms = (await _roomRepository.GetAll()).ToList();

            if (!rooms.Any())
                throw new Exception("No rooms found");

            if (request.Filters != null)
                rooms = ApplyFilters(request.Filters, rooms);

            if (request.SortBy.HasValue)
                rooms = ApplySort(request.SortBy.Value, rooms);

            if (request.Pagination != null)
                rooms = ApplyPagination(request.Pagination, rooms);

            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        private List<Room> ApplyFilters(RoomFilter filters, List<Room> rooms)
        {
            if (!string.IsNullOrEmpty(filters.Type))
                rooms = rooms.Where(r => r.Type.ToLower().Contains(filters.Type.ToLower())).ToList();

            if (filters.IsAvailable.HasValue)
                rooms = rooms.Where(r => r.IsAvailable == filters.IsAvailable).ToList();

            if (filters.MinCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= filters.MinCapacity).ToList();

            if (filters.PriceRange != null)
                rooms = rooms.Where(r => r.PricePerNight >= filters.PriceRange.Min && r.PricePerNight <= filters.PriceRange.Max).ToList();

            return rooms;
        }

        private List<Room> ApplySort(int sortBy, List<Room> rooms)
        {
            return sortBy switch
            {
                1 => rooms.OrderBy(r => r.PricePerNight).ToList(),
                -1 => rooms.OrderByDescending(r => r.PricePerNight).ToList(),
                2 => rooms.OrderBy(r => r.Capacity).ToList(),
                -2 => rooms.OrderByDescending(r => r.Capacity).ToList(),
                3 => rooms.OrderBy(r => r.Type).ToList(),
                -3 => rooms.OrderByDescending(r => r.Type).ToList(),
                4 => rooms.OrderBy(r => r.RoomId).ToList()
            };
        }

        private List<Room> ApplyPagination(Pagination pagination, List<Room> rooms)
        {
            return rooms
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
        }

    }
}
