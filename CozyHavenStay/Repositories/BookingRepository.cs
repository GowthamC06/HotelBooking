using CozyHavenStay.Contexts;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
{
    public class BookingRepository : Repository<int, Booking>
    {
        public BookingRepository(CozyHavenStayContext context) : base(context) { }

        public override async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings
                .Include(b => b.Room).ThenInclude(r => r.Hotel)
                .Include(b => b.Customer)
                .Include(b => b.Payments)
                .ToListAsync();
        }

        public override async Task<Booking?> GetById(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room).ThenInclude(r => r.Hotel)
                .Include(b => b.Customer)
                .Include(b => b.Payments)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }
    }
}
