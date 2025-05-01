using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStay.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Customer> _customerRepository;

        public CustomerService(IRepository<string, User> userRepository, IRepository<int, Customer> customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task<CreateCustomerResponse> AddCustomer(CreateCustomerRequest request)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            var user = new User
            {
                Email = request.Email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = "Customer"
            };

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var customer = new Customer
            {
                Name = request.Name,
                Gender = request.Gender,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                User = userResult
            };

            var result = await _customerRepository.Add(customer);
            if (result == null)
                throw new Exception("Failed to create customer");

            return new CreateCustomerResponse { Id = result.Id };
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers() => await _customerRepository.GetAll();

        public async Task<Customer?> GetCustomerById(int id) => await _customerRepository.GetById(id);

        public async Task<Customer?> UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null) return null;

            customer.Name = request.Name;
            customer.Phone = request.Phone;
            customer.Address = request.Address;

            return await _customerRepository.Update(id, customer);
        }

        public async Task<Customer?> DeleteCustomer(int id) => await _customerRepository.Delete(id);
    }
}
