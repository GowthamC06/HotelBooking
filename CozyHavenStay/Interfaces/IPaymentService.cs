using CozyHavenStay.Models.DTOs;
namespace CozyHavenStay.Interfaces
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponse> ProcessPayment(CreatePaymentRequest request);
        Task<PaymentResponse?> GetPaymentById(int id);
        Task<IEnumerable<PaymentResponse>> GetPaymentsByBooking(int bookingId);
        Task<IEnumerable<PaymentResponse>> GetAllPayments();
        Task<IEnumerable<PaymentResponse>> GetPaymentsByFilter(PaymentFilterRequest request);
        Task<PaymentStatsResponse> GetPaymentStats();
    }
}
