using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // POST: api/Room
        [Authorize(Roles = "Admin,HotelManager")]
        [HttpPost]
        public async Task<ActionResult<CreateRoomResponse>> AddRoom([FromBody] CreateRoomRequest request)
        {
            try
            {
                var response = await _roomService.AddRoom(request);
                return CreatedAtAction(nameof(GetRoomById), new { id = response.RoomId }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Room/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomResponse>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
                return NotFound("Room not found");

            return Ok(room);
        }

        // GET: api/Room/hotel/5
        [Authorize]
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoomsByHotel(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotel(hotelId);
            return Ok(rooms);
        }

        // POST: api/Room/filter
        [Authorize]
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoomsByFilter([FromBody] RoomRequest request)
        {
            try
            {
                var rooms = await _roomService.GetRoomsByFilter(request);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Room/{id}
        [Authorize(Roles = "Admin,HotelManager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponse>> UpdateRoom(int id, [FromBody] CreateRoomRequest request)
        {
            try
            {
                var updated = await _roomService.UpdateRoom(id, request);
                if (updated == null)
                    return NotFound("Room not found");

                var response = await _roomService.GetRoomById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Room/{id}
        [Authorize(Roles = "Admin,HotelManager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteRoom(int id)
        {
            var deleted = await _roomService.DeleteRoom(id);
            if (deleted == null)
                return NotFound("Room not found");

            return Ok("Room deleted successfully");
        }
    }
}
