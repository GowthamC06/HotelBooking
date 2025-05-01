using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/Booking
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult<CreateBookingResponse>> CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                var result = await _bookingService.AddBooking(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Booking/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(int id)
        {
            var result = await _bookingService.GetBookingById(id);
            if (result == null)
                return NotFound("Booking not found");
            return Ok(result);
        }

        // GET: api/Booking/customer/1
        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByCustomer(int customerId)
        {
            var result = await _bookingService.GetBookingsByCustomer(customerId);
            return Ok(result);
        }

        // GET: api/Booking/hotel/1
        [Authorize(Roles = "HotelManager")]
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookingsByHotel(int hotelId)
        {
            var result = await _bookingService.GetBookingsByHotel(hotelId);
            return Ok(result);
        }

        // POST: api/Booking/cancel
        [Authorize(Roles = "Customer")]
        [HttpPost("cancel")]
        public async Task<ActionResult<CancelBookingResponse>> CancelBooking([FromBody] CancelBookingRequest request)
        {
            try
            {
                var result = await _bookingService.CancelBooking(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Booking/check-conflict
        [Authorize(Roles = "Customer")]
        [HttpPost("check-conflict")]
        public async Task<ActionResult<BookingConflictCheckResponse>> CheckConflict([FromBody] BookingConflictCheckRequest request)
        {
            var result = await _bookingService.CheckBookingConflict(request);
            return Ok(result);
        }

        // POST: api/Booking/filter
        [Authorize(Roles = "Admin,HotelManager")]
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> FilterBookings([FromBody] BookingRequest request)
        {
            try
            {
                var result = await _bookingService.GetBookingsByFilter(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
