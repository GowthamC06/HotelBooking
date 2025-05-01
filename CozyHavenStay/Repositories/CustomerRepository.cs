using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Contexts;

namespace CozyHavenStay.Repositories
{
    public class CustomerRepository : Repository<int, Customer>
    {
        public CustomerRepository(CozyHavenStayContext context) : base(context)
        {
        }
        public override async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers
                .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                throw new Exception($"Customer with ID {id} not found");

            return customer;
        }
        public override async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = _context.Customers.Include(c => c.User);
            if (customers.Count() == 0)
                throw new Exception("No customers found");
            return customers;
        }
    }
}
