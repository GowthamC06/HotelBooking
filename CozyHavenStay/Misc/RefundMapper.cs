using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using AutoMapper;

namespace CozyHavenStay.Misc
{
    public class RefundMapper : Profile
    {
        public RefundMapper()
        {
            CreateMap<CreateRefundRequest, Refund>();
            CreateMap<Refund, CreateRefundResponse>();
            CreateMap<Refund, RefundResponse>();
        }
    }
}
