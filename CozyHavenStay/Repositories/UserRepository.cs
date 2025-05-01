using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Contexts;

namespace CozyHavenStay.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        public UserRepository(CozyHavenStayContext context) : base(context) { }

        public async override Task<IEnumerable<User>> GetAll()
        {
            var users = _context.Users;
            if (users.Count() == 0)
            {
                throw new Exception("No users found");
            }
            return users;
        }

       

        public override async Task<User> GetById(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

    }
}
