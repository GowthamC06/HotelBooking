using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Interfaces
{
    public interface IRefundService
    {
        Task<CreateRefundResponse> CreateRefund(CreateRefundRequest request);
        Task<IEnumerable<RefundResponse>> GetRefundsByPayment(int paymentId);
    }
}
