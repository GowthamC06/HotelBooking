using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using AutoMapper;

namespace CozyHavenStay.Misc
{
    public class ReviewMapper : Profile
    {
        public ReviewMapper() 
        {
            CreateMap<CreateReviewRequest, Review>();
            CreateMap<Review, CreateReviewResponse>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Review, ReviewResponse>();
        }
    }
}
