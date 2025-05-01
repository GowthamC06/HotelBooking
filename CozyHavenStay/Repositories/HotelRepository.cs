using CozyHavenStay.Models;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
{
    public class HotelRepository : Repository<int,Hotel>
    {

        public HotelRepository(CozyHavenStayContext context) : base(context)
        {
        }

        public async Task<Hotel?> Add(Hotel entity)
        {
            _context.Hotels.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Hotel?> Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return null;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public override async Task<IEnumerable<Hotel>> GetAll()
        {
            return await _context.Hotels
                                 .Include(h => h.Rooms)
                                 .Include(h => h.Reviews)
                                 .Include(h => h.HotelManager)
                                 .ToListAsync();
        }

        public override async Task<Hotel?> GetById(int id)
        {
            return await _context.Hotels
                                 .Include(h => h.Rooms)
                                 .Include(h => h.Reviews)
                                 .Include(h => h.HotelManager)
                                 .FirstOrDefaultAsync(h => h.HotelId == id);
        }

        public async Task<Hotel?> Update(Hotel entity)
        {
            var existingHotel = await _context.Hotels.FindAsync(entity.HotelId);
            if (existingHotel == null) return null;

            _context.Entry(existingHotel).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingHotel;
        }
    }
}
