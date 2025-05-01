using Microsoft.AspNetCore.Mvc;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelManagerController : ControllerBase
    {
        private readonly IHotelManagerService _hotelManagerService;

        public HotelManagerController(IHotelManagerService hotelManagerService)
        {
            _hotelManagerService = hotelManagerService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateHotelManagerResponse>> Create(CreateHotelManagerRequest request)
        {
            var result = await _hotelManagerService.CreateHotelManager(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<HotelManager>>> GetAll()
        {
            var managers = await _hotelManagerService.GetAllHotelManagers();
            return Ok(managers);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HotelManager")]
        public async Task<ActionResult<HotelManager>> GetById(int id)
        {
            var manager = await _hotelManagerService.GetManagerById(id);
            if (manager == null) return NotFound();
            return Ok(manager);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HotelManager")]
        public async Task<ActionResult<HotelManager>> Update(int id, UpdateHotelManagerRequest request)
        {
            var updated = await _hotelManagerService.UpdateHotelManager(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HotelManager>> Delete(int id)
        {
            var deleted = await _hotelManagerService.DeleteHotelManager(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
