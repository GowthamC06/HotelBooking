using CozyHavenStay.Interfaces;
using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using CozyHavenStay.Repositories;
using System.Security.Cryptography;

namespace CozyHavenStay.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<int, HotelManager> _hotelManagerRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IRepository<string, User> userRpository,
                                     IRepository<int, Customer> customerRepository,
                                     IRepository<int, Admin> adminRepository,
                                     IRepository<int, HotelManager> hotelManagerRepository,
                                     ITokenService tokenService)
        {
            _userRepository = userRpository;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _adminRepository = adminRepository;
            _hotelManagerRepository = hotelManagerRepository;
        }

        public async Task<LoginResponse> Login(UserLoginRequest loginRequest)
        {
            var user = await _userRepository.GetById(loginRequest.Email);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            HMACSHA512 hmac = new HMACSHA512(user.HashKey);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginRequest.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new UnauthorizedAccessException("Invalid password");
            }

            string name = "";
            int id = 0;

            if (user.Role == "Customer")
            {
                var customer = (await _customerRepository.GetAll()).FirstOrDefault(c => c.Email == loginRequest.Email);
                if (customer == null)
                    throw new UnauthorizedAccessException("Customer not found");
                name = customer.Name;
                id = customer.Id;
            }
            else if (user.Role == "Admin")
            {
                // Similar logic if you have AdminRepository
                var admin = (await _adminRepository.GetAll()).FirstOrDefault(c => c.Email == loginRequest.Email);
                // or query from admin repo
                if (admin == null)
                    throw new UnauthorizedAccessException("Admin not found");
                name = admin.Name;
                id = admin.Id;
            }
            else if (user.Role == "HotelManager")
            {
                var manager = (await _hotelManagerRepository.GetAll()).FirstOrDefault(c => c.Email == loginRequest.Email);
                if (manager == null)
                    throw new UnauthorizedAccessException("Hotel Manager not found");
                name = manager.Name;
                id = manager.Id;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid role");
            }


            var token = await _tokenService.GenerateToken(id, name, user.Role);
            return new LoginResponse { Id = id, Name = name, Role = user.Role, Token = token };
        }
    }
}