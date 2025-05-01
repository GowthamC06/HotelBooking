using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Contexts;

namespace CozyHavenStay.Repositories

{
    public class HotelManagerRepository : Repository<int, HotelManager>
    {
        public HotelManagerRepository(CozyHavenStayContext context) : base(context)
        {
        }

        public override async Task<HotelManager> GetById(int id)
        {
            var manager = await _context.HotelManagers
                .SingleOrDefaultAsync(rm => rm.Id == id);

            if (manager == null)
                throw new Exception($"Hotel Manager with ID {id} not found");

            return manager;
        }

        public override async Task<IEnumerable<HotelManager>> GetAll()
        {
            var managers = await _context.HotelManagers
                .Include(rm => rm.Hotel)
                .ToListAsync();

            if (managers.Count == 0)
                throw new Exception("No hotel managers found");

            return managers;
        }
    }
}
