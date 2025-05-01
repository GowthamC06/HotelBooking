using CozyHavenStay.Models;
using AutoMapper;
using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Misc
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<CreatePaymentRequest, Payment>();
            CreateMap<Payment, CreatePaymentResponse>();
            CreateMap<Payment, PaymentResponse>();
        }
    }
}
