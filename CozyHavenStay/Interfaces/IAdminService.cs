using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using CozyHavenStay.Repositories;
namespace CozyHavenStay.Interfaces
{
    public interface IAdminService
    {
        Task<CreateAdminResponse> CreateAdmin(CreateAdminRequest request);
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task<Admin?> GetAdminById(int id);
        Task<Admin?> UpdateAdmin(int id, UpdateAdminRequest request);
        Task<Admin?> DeleteAdmin(int id);
    }
}
