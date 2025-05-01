using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(UserLoginRequest loginRequest)
        {
            try
            {
                var result = await _authenticationService.Login(loginRequest);
                if (result.Role == "Customer")
                {
                    return Ok(new
                    {
                        Message = "Customer login successful",
                        Data = result
                    });
                }
                else if (result.Role == "Admin")
                {
                    return Ok(new
                    {
                        Message = "Admin login successful",
                        Data = result
                    });
                }
                else if (result.Role == "HotelManager")
                {
                    return Ok(new
                    {
                        Message = "Hotel Manager login successful",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest("Invalid role.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}