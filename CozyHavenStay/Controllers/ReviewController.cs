using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult<CreateReviewResponse>> AddReview([FromBody] CreateReviewRequest request)
        {
            try
            {
                var response = await _reviewService.AddReview(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound("Review not found");

            return Ok(review);
        }

        [Authorize]
        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByHotel(int hotelId)
        {
            var reviews = await _reviewService.GetReviewsByHotel(hotelId);
            return Ok(reviews);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteReview(int id)
        {
            var deleted = await _reviewService.DeleteReview(id);
            if (deleted == null)
                return NotFound("Review not found");

            return Ok("Review deleted successfully");
        }
    }
}
