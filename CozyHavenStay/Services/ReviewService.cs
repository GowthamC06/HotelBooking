using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<int, Review> _reviewRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IMapper _mapper;

        public ReviewService(IRepository<int, Review> reviewRepository,
                             IRepository<int, Customer> customerRepository,
                             IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CreateReviewResponse> AddReview(CreateReviewRequest request)
        {
            var review = _mapper.Map<Review>(request);
            review.Date = DateTime.UtcNow;

            var result = await _reviewRepository.Add(review);
            if (result == null)
                throw new Exception("Failed to add review");

            return new CreateReviewResponse
            {
                ReviewId = result.ReviewId,
                Message = "Review added successfully"
            };
        }

        public async Task<ReviewResponse?> GetReviewById(int id)
        {
            var review = await _reviewRepository.GetById(id);
            if (review == null) return null;

            var response = _mapper.Map<ReviewResponse>(review);

            var customer = await _customerRepository.GetById(review.CustomerId);
            if (customer != null)
                response.CustomerName = customer.Name;

            return response;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByHotel(int hotelId)
        {
            var reviews = await _reviewRepository.GetAll();
            var filtered = reviews.Where(r => r.HotelId == hotelId);
            return _mapper.Map<IEnumerable<ReviewDTO>>(filtered);
        }

        public async Task<Review?> DeleteReview(int id)
        {
            return await _reviewRepository.Delete(id);
        }
    }
}
