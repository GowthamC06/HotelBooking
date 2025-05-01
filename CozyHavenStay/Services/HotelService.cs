using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;


namespace CozyHavenStay.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<int, Hotel> _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IRepository<int, Hotel> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<CreateHotelResponse> AddHotel(CreateHotelRequest request)
        {
            var hotel = _mapper.Map<Hotel>(request);
            var result = await _hotelRepository.Add(hotel);

            if (result == null)
                throw new Exception("Failed to create hotel");

            return new CreateHotelResponse
            {
                HotelId = result.HotelId,
                Message = "Hotel created successfully"
            };
        }

        public async Task<HotelResponse?> GetHotelById(int id)
        {
            var hotel = await _hotelRepository.GetById(id);
            if (hotel == null) return null;

            return _mapper.Map<HotelResponse>(hotel);
        }

        public async Task<IEnumerable<HotelDTO>> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAll();
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        public async Task<Hotel?> UpdateHotel(int id, CreateHotelRequest request)
        {
            var existingHotel = await _hotelRepository.GetById(id);
            if (existingHotel == null)
                throw new Exception("Hotel not found");

            var updatedHotel = _mapper.Map<Hotel>(request);
            updatedHotel.HotelId = id;

            return await _hotelRepository.Update(id, updatedHotel);
        }

        public async Task<Hotel?> DeleteHotel(int id)
        {
            return await _hotelRepository.Delete(id);
        }
        public async Task<IEnumerable<HotelDTO>> GetHotelsByFilter(HotelRequest request)
        {
            var hotels = (await _hotelRepository.GetAll()).ToList();

            if (!hotels.Any())
                throw new Exception("No hotels found");

            if (request.Filters != null)
                hotels = ApplyFilters(request.Filters, hotels);

            //if (request.SortBy != null)
            //    hotels = ApplySort(request.SortBy.Value, hotels);

            //if (request.Pagination != null)
            //    hotels = ApplyPagination(request.Pagination, hotels);

            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        private List<Hotel> ApplyFilters(HotelFilter filters, List<Hotel> hotels)
        {
            if (!string.IsNullOrEmpty(filters.Location))
                hotels = hotels
                    .Where(h => h.Location.ToLower() == filters.Location.ToLower())
                    .ToList();

            return hotels;
        }


        private List<Hotel> ApplySort(int sortBy, List<Hotel> hotels)
        {
            return sortBy switch
            {
                1 => hotels.OrderBy(h => h.HotelName).ToList(),
                -1 => hotels.OrderByDescending(h => h.HotelName).ToList(),
                2 => hotels.OrderBy(h => h.StarRating).ToList(),
                -2 => hotels.OrderByDescending(h => h.StarRating).ToList(),
               3 => hotels.OrderBy(h => h.HotelId).ToList(),
            };
        }

        private List<Hotel> ApplyPagination(Pagination pagination, List<Hotel> hotels)
        {
            return hotels
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
        }

    }
}
