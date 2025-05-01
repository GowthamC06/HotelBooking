using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStay.Services
{
    public class HotelManagerService : IHotelManagerService
    {
        private readonly IRepository<int, HotelManager> _managerRepository;
        private readonly IRepository<string, User> _userRepository;

        public HotelManagerService(IRepository<int, HotelManager> managerRepository, IRepository<string, User> userRepository)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateHotelManagerResponse> CreateHotelManager(CreateHotelManagerRequest request)
        {
            var existingUser = await _userRepository.GetAll();
            if (existingUser.Any(u => u.Email == request.Email))
                throw new Exception("A user with this email already exists.");

            using var hmac = new HMACSHA512();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            var user = new User
            {
                Email = request.Email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = "HotelManager"
            };

            var userResult = await _userRepository.Add(user);
            if (userResult == null)
                throw new Exception("Failed to create user");

            var manager = new HotelManager
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)),
                User = userResult
            };

            var result = await _managerRepository.Add(manager);
            if (result == null)
                throw new Exception("Failed to create manager");

            return new CreateHotelManagerResponse { Id = result.Id };
        }

        public async Task<IEnumerable<HotelManager>> GetAllHotelManagers() => await _managerRepository.GetAll();

        public async Task<HotelManager?> GetManagerById(int id) => await _managerRepository.GetById(id);

        public async Task<HotelManager?> UpdateHotelManager(int id, UpdateHotelManagerRequest request)
        {
            var manager = await _managerRepository.GetById(id);
            if (manager == null) return null;

            manager.Name = request.Name;
            manager.Phone = request.Phone;

            return await _managerRepository.Update(id, manager);
        }

        public async Task<HotelManager?> DeleteHotelManager(int id) => await _managerRepository.Delete(id);
    }
}

