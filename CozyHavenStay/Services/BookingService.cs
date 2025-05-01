using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Room> _roomRepository;
        private readonly IMapper _mapper;

        public BookingService(IRepository<int, Booking> bookingRepository,
                              IRepository<int, Room> roomRepository,
                              IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<CreateBookingResponse> AddBooking(CreateBookingRequest request)
        {
            // Check for conflict first
            var conflict = await CheckBookingConflict(new BookingConflictCheckRequest
            {
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate
            });

            if (conflict.HasConflict)
                throw new Exception("Booking dates conflict with an existing booking.");

            var room = await _roomRepository.GetById(request.RoomId);
            if (room == null || !room.IsAvailable)
                throw new Exception("Room not available");

            var booking = _mapper.Map<Booking>(request);
            booking.Status = "Confirmed";
            booking.CreatedAt = DateTime.UtcNow;

            var result = await _bookingRepository.Add(booking);

            // Optionally make room unavailable
            room.IsAvailable = false;
            await _roomRepository.Update(room.RoomId, room);

            return new CreateBookingResponse
            {
                BookingId = result.BookingId,
                Message = "Booking confirmed"
            };
        }

        public async Task<BookingResponse?> GetBookingById(int bookingId)
        {
            var booking = await _bookingRepository.GetById(bookingId);
            if (booking == null) return null;

            var dto = _mapper.Map<BookingResponse>(booking);
            dto.RoomType = booking.Room?.Type ?? "";
            dto.HotelName = booking.Room?.Hotel?.HotelName ?? "";
            return dto;
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByCustomer(int customerId)
        {
            var all = await _bookingRepository.GetAll();
            var filtered = all.Where(b => b.CustomerId == customerId);
            return _mapper.Map<IEnumerable<BookingDTO>>(filtered);
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByHotel(int hotelId)
        {
            var all = await _bookingRepository.GetAll();
            var filtered = all.Where(b => b.Room?.HotelId == hotelId);
            return _mapper.Map<IEnumerable<BookingDTO>>(filtered);
        }

        public async Task<CancelBookingResponse> CancelBooking(CancelBookingRequest request)
        {
            var booking = await _bookingRepository.GetById(request.BookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status == "Cancelled")
                return new CancelBookingResponse
                {
                    BookingId = booking.BookingId,
                    Status = "Cancelled",
                    Message = "Booking is already cancelled"
                };

            booking.Status = "Cancelled";
            await _bookingRepository.Update(booking.BookingId, booking);

            if (booking.Room != null)
            {
                booking.Room.IsAvailable = true;
                await _roomRepository.Update(booking.Room.RoomId, booking.Room);
            }

            return new CancelBookingResponse
            {
                BookingId = booking.BookingId,
                Status = "Cancelled",
                Message = "Booking has been cancelled successfully"
            };
        }

        public async Task<BookingConflictCheckResponse> CheckBookingConflict(BookingConflictCheckRequest request)
        {
            var bookings = await _bookingRepository.GetAll();

            var hasConflict = bookings.Any(b =>
                b.RoomId == request.RoomId &&
                b.Status != "Cancelled" &&
                request.CheckInDate < b.CheckOutDate &&
                request.CheckOutDate > b.CheckInDate
            );

            return new BookingConflictCheckResponse
            {
                HasConflict = hasConflict,
                Message = hasConflict ? "Conflicting booking exists" : "Room is available"
            };
        }
        public async Task<IEnumerable<BookingDTO>> GetBookingsByFilter(BookingRequest request)
        {
            var bookings = (await _bookingRepository.GetAll()).ToList();

            if (!bookings.Any())
                throw new Exception("No bookings found");

            if (request.Filters != null)
                bookings = ApplyFilters(request.Filters, bookings);

            if (request.SortBy != null)
                bookings = ApplySort(request.SortBy.Value, bookings);

            if (request.Pagination != null)
                bookings = ApplyPagination(request.Pagination, bookings);

            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        private List<Booking> ApplyFilters(BookingFilter filters, List<Booking> bookings)
        {
            if (!string.IsNullOrEmpty(filters.Status))
                bookings = bookings.Where(b => b.Status.ToLower() == filters.Status.ToLower()).ToList();

            if (filters.FromDate.HasValue)
                bookings = bookings.Where(b => b.CheckInDate >= filters.FromDate.Value).ToList();

            if (filters.ToDate.HasValue)
                bookings = bookings.Where(b => b.CheckOutDate <= filters.ToDate.Value).ToList();

            if (filters.CustomerId.HasValue)
                bookings = bookings.Where(b => b.CustomerId == filters.CustomerId.Value).ToList();

            if (filters.HotelId.HasValue)
                bookings = bookings.Where(b => b.Room != null && b.Room.HotelId == filters.HotelId.Value).ToList();

            return bookings;
        }

        private List<Booking> ApplySort(int sortBy, List<Booking> bookings)
        {
            return sortBy switch
            {
                1 => bookings.OrderBy(b => b.CheckInDate).ToList(),
                -1 => bookings.OrderByDescending(b => b.CheckInDate).ToList(),
                2 => bookings.OrderBy(b => b.Status).ToList(),
                -2 => bookings.OrderByDescending(b => b.Status).ToList(),
                3 => bookings.OrderBy(b => b.BookingId).ToList(),
            };
        }

        private List<Booking> ApplyPagination(Pagination pagination, List<Booking> bookings)
        {
            return bookings
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
        }
        //public async Task<BookingConflictCheckResponse> CheckBookingConflict(BookingConflictCheckRequest request)
        //{
        //    var bookings = await _bookingRepository.GetAll();

        //    var hasConflict = bookings.Any(b =>
        //        b.RoomId == request.RoomId &&
        //        b.Status != "Cancelled" &&
        //        request.CheckInDate < b.CheckOutDate &&
        //        request.CheckOutDate > b.CheckInDate
        //    );

        //    return new BookingConflictCheckResponse
        //    {
        //        HasConflict = hasConflict,
        //        Message = hasConflict ? "Conflicting booking exists" : "Room is available"
        //    };
        //}


    }
}
