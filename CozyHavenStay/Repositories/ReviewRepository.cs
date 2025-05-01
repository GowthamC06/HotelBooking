using CozyHavenStay.Contexts;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Repositories
{
    public class ReviewRepository : Repository<int, Review>
    {
        public ReviewRepository(CozyHavenStayContext context) : base(context) { }

        public override async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews
                                 .Include(r => r.Customer)
                                 .Include(r => r.Hotel)
                                 .ToListAsync();
        }

        public override async Task<Review?> GetById(int id)
        {
            return await _context.Reviews
                                 .Include(r => r.Customer)
                                 .Include(r => r.Hotel)
                                 .FirstOrDefaultAsync(r => r.ReviewId == id);
        }
    }
}
