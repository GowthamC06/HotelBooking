using CozyHavenStay.Contexts;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
{
    public class RefundRepository : Repository<int, Refund>
    {
        public RefundRepository(CozyHavenStayContext context) : base(context) { }

        public override async Task<IEnumerable<Refund>> GetAll()
        {
            return await _context.Refunds
                .Include(r => r.Payment)
                .ToListAsync();
        }

        public override async Task<Refund?> GetById(int id)
        {
            return await _context.Refunds
                .Include(r => r.Payment)
                .FirstOrDefaultAsync(r => r.RefundId == id);
        }
    }

}
