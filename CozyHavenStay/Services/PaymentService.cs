using AutoMapper;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;

namespace CozyHavenStay.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<int, Payment> _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<int, Payment> paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaymentResponse> ProcessPayment(CreatePaymentRequest request)
        {
            var payment = _mapper.Map<Payment>(request);
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = "Success";

            var result = await _paymentRepository.Add(payment);
            return _mapper.Map<CreatePaymentResponse>(result);
        }

        public async Task<PaymentResponse?> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.GetById(id);
            return payment == null ? null : _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<IEnumerable<PaymentResponse>> GetPaymentsByBooking(int bookingId)
        {
            var payments = await _paymentRepository.GetAll();
            var filtered = payments.Where(p => p.BookingId == bookingId);
            return _mapper.Map<IEnumerable<PaymentResponse>>(filtered);
        }

        public async Task<IEnumerable<PaymentResponse>> GetAllPayments()
        {
            var all = await _paymentRepository.GetAll();
            return _mapper.Map<IEnumerable<PaymentResponse>>(all);
        }
        public async Task<IEnumerable<PaymentResponse>> GetPaymentsByFilter(PaymentFilterRequest request)
        {
            var payments = (await _paymentRepository.GetAll()).ToList();

            if (!string.IsNullOrEmpty(request.Method))
                payments = payments.Where(p => p.PaymentMethod.ToLower() == request.Method.ToLower()).ToList();

            if (!string.IsNullOrEmpty(request.Status))
                payments = payments.Where(p => p.Status.ToLower() == request.Status.ToLower()).ToList();

            if (request.FromDate.HasValue)
                payments = payments.Where(p => p.PaymentDate >= request.FromDate.Value).ToList();

            if (request.ToDate.HasValue)
                payments = payments.Where(p => p.PaymentDate <= request.ToDate.Value).ToList();

            return _mapper.Map<IEnumerable<PaymentResponse>>(payments);
        }
        public async Task<PaymentStatsResponse> GetPaymentStats()
        {
            var payments = await _paymentRepository.GetAll();
            var successful = payments.Where(p => p.Status == "Success");

            var totalRevenue = successful.Sum(p => p.AmountPaid);
            var todayRevenue = successful
                .Where(p => p.PaymentDate.Date == DateTime.UtcNow.Date)
                .Sum(p => p.AmountPaid);

            var methodCount = successful
                .GroupBy(p => p.PaymentMethod)
                .ToDictionary(g => g.Key, g => g.Count());

            return new PaymentStatsResponse
            {
                TotalRevenue = totalRevenue,
                TodayRevenue = todayRevenue,
                PaymentMethodCount = methodCount
            };
        }


    }
}