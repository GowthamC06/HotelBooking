using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // POST: api/Hotel

        [HttpPost]
        public async Task<ActionResult<CreateHotelResponse>> CreateHotel([FromBody] CreateHotelRequest request)
        {
            try
            {
                var response = await _hotelService.AddHotel(request);
                return CreatedAtAction(nameof(GetHotelById), new { id = response.HotelId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Hotel/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelResponse>> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel == null)
                return NotFound("Hotel not found");

            return Ok(hotel);
        }

        // GET: api/Hotel/all
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        // POST: api/Hotel/filter
      
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotelsByFilter([FromBody] HotelRequest request)
        {
            try
            {
                var hotels = await _hotelService.GetHotelsByFilter(request);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Hotel/{id}

        [HttpPut("{id}")]
        public async Task<ActionResult<HotelResponse>> UpdateHotel(int id, [FromBody] CreateHotelRequest request)
        {
            try
            {
                var result = await _hotelService.UpdateHotel(id, request);
                if (result == null)
                    return NotFound("Hotel not found");

                var updatedResponse = await _hotelService.GetHotelById(id);
                return Ok(updatedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Hotel/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            if (result == null)
                return NotFound("Hotel not found");

            return Ok("Hotel deleted successfully");
        }
    }
}
