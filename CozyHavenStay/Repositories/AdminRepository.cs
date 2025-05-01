using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Contexts;

namespace CozyHavenStay.Repositories
{
    public class AdminRepository : Repository<int, Admin>
    {
        public AdminRepository(CozyHavenStayContext context) : base(context)
        {
        }

        public override async Task<Admin> GetById(int key)
        {
            var admin = await _context.Admins.FindAsync(key);
            if (admin == null)
                throw new Exception($"Admin with ID {key} not found");
            return admin;
        }

        public override async Task<IEnumerable<Admin>> GetAll()
        {
            var admins = await _context.Admins.ToListAsync();
            if (!admins.Any())
                throw new Exception("No admins found");
            return admins;
        }
    }
}
