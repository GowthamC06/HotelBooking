using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Interfaces
{
    public interface IBookingService
    {
        Task<CreateBookingResponse> AddBooking(CreateBookingRequest request);
        Task<BookingResponse?> GetBookingById(int bookingId);
        Task<IEnumerable<BookingDTO>> GetBookingsByCustomer(int customerId);
        Task<IEnumerable<BookingDTO>> GetBookingsByHotel(int hotelId);
        Task<CancelBookingResponse> CancelBooking(CancelBookingRequest request);
        Task<BookingConflictCheckResponse> CheckBookingConflict(BookingConflictCheckRequest request);
        Task<IEnumerable<BookingDTO>> GetBookingsByFilter(BookingRequest request);
        }
}
