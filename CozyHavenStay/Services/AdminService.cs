using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStay.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Admin> _adminRepository;

        public AdminService(IRepository<string, User> userRepository, IRepository<int, Admin> adminRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }

        public async Task<CreateAdminResponse> CreateAdmin(CreateAdminRequest request)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            var user = new User
            {
                Email = request.Email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = "Admin"
            };

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var admin = new Admin
            {
                Name = request.Name,
                Email = request.Email,
                User = userResult
            };

            var adminResult = await _adminRepository.Add(admin);
            if (adminResult == null)
                throw new Exception("Failed to create admin");

            return new CreateAdminResponse { Id = adminResult.Id };
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins() => await _adminRepository.GetAll();

        public async Task<Admin?> GetAdminById(int id) => await _adminRepository.GetById(id);

        public async Task<Admin?> UpdateAdmin(int id, UpdateAdminRequest request)
        {
            var admin = await _adminRepository.GetById(id);
            if (admin == null) return null;

            admin.Name = request.Name;
            admin.Email = request.Email;

            return await _adminRepository.Update(id, admin);
        }

        public async Task<Admin?> DeleteAdmin(int id) => await _adminRepository.Delete(id);
    }
}
