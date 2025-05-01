using CozyHavenStay.Contexts;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
    {
        public class PaymentRepository : Repository<int, Payment>
        {
            public PaymentRepository(CozyHavenStayContext context) : base(context) { }

            public override async Task<IEnumerable<Payment>> GetAll()
            {
                return await _context.Payments
                                     .Include(p => p.Booking)
                                     .ThenInclude(b => b.Room)
                                     .ToListAsync();
            }

            public override async Task<Payment?> GetById(int id)
            {
                return await _context.Payments
                                     .Include(p => p.Booking)
                                     .ThenInclude(b => b.Room)
                                     .FirstOrDefaultAsync(p => p.PaymentId == id);
            }
        }
    }


