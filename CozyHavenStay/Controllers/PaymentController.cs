using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST: api/Payment
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var response = await _paymentService.ProcessPayment(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Payment/{id}
        [Authorize(Roles = "Admin,HotelManager,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetPaymentById(int id)
        {
            var result = await _paymentService.GetPaymentById(id);
            if (result == null)
                return NotFound("Payment not found");

            return Ok(result);
        }

        // GET: api/Payment/booking/{bookingId}
        [Authorize(Roles = "Admin,HotelManager,Customer")]
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentsByBooking(int bookingId)
        {
            var result = await _paymentService.GetPaymentsByBooking(bookingId);
            return Ok(result);
        }

        // GET: api/Payment/all
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetAllPayments()
        {
            var result = await _paymentService.GetAllPayments();
            return Ok(result);
        }

        // GET: api/Payment/stats
        [Authorize(Roles = "Admin")]
        [HttpGet("stats")]
        public async Task<ActionResult<PaymentStatsResponse>> GetStats()
        {
            var result = await _paymentService.GetPaymentStats();
            return Ok(result);
        }

        // POST: api/Payment/filter
        [Authorize(Roles = "Admin,HotelManager")]
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> FilterPayments([FromBody] PaymentFilterRequest request)
        {
            var result = await _paymentService.GetPaymentsByFilter(request);
            return Ok(result);
        }
    }
}
