using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService _refundService;

        public RefundController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [Authorize(Roles = "Admin,HotelManager")]
        [HttpPost]
        public async Task<ActionResult<CreateRefundResponse>> CreateRefund(CreateRefundRequest request)
        {
            try
            {
                var response = await _refundService.CreateRefund(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,HotelManager,Customer")]
        [HttpGet("payment/{paymentId}")]
        public async Task<ActionResult<IEnumerable<RefundResponse>>> GetRefundsByPayment(int paymentId)
        {
            return Ok(await _refundService.GetRefundsByPayment(paymentId));
        }
    }

}
