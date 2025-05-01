using CozyHavenStay.Contexts;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
{
    public class RoomRepository : Repository<int, Room>
    {
        public RoomRepository(CozyHavenStayContext context) : base(context) { }

        public override async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms
                                 .Include(r => r.Hotel)
                                 .Include(r => r.Bookings)
                                 .ToListAsync();
        }

        public override async Task<Room?> GetById(int id)
        {
            return await _context.Rooms
                                 .Include(r => r.Hotel)
                                 .Include(r => r.Bookings)
                                 .FirstOrDefaultAsync(r => r.RoomId == id);
        }
    }
}
