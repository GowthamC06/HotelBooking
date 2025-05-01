using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;

namespace CozyHavenStay.Interfaces
{
    public interface IReviewService
    {
        Task<CreateReviewResponse> AddReview(CreateReviewRequest request);
        Task<ReviewResponse?> GetReviewById(int id);
        Task<IEnumerable<ReviewDTO>> GetReviewsByHotel(int hotelId);
        Task<Review?> DeleteReview(int id);
    }
}
