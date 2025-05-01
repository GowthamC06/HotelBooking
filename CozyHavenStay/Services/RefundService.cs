using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;

namespace CozyHavenStay.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRepository<int, Refund> _refundRepo;
        private readonly IRepository<int, Payment> _paymentRepo;
        private readonly IMapper _mapper;

        public RefundService(IRepository<int, Refund> refundRepo, IRepository<int, Payment> paymentRepo, IMapper mapper)
        {
            _refundRepo = refundRepo;
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<CreateRefundResponse> CreateRefund(CreateRefundRequest request)
        {
            var payment = await _paymentRepo.GetById(request.PaymentId);
            if (payment == null || payment.Status == "Refunded")
                throw new Exception("Invalid or already refunded payment.");

            var refund = _mapper.Map<Refund>(request);
            refund.Status = "Refunded";
            refund.RefundDate = DateTime.UtcNow;

            // Update payment status
            payment.Status = "Refunded";
            await _paymentRepo.Update(payment.PaymentId, payment);

            var result = await _refundRepo.Add(refund);
            return _mapper.Map<CreateRefundResponse>(result);
        }

        public async Task<IEnumerable<RefundResponse>> GetRefundsByPayment(int paymentId)
        {
            var all = await _refundRepo.GetAll();
            var filtered = all.Where(r => r.PaymentId == paymentId);
            return _mapper.Map<IEnumerable<RefundResponse>>(filtered);
        }
    }

}
