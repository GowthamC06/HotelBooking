using CozyHavenStay.Models.DTOs;
using CozyHavenStay.Models;
using System.Threading.Tasks;
namespace CozyHavenStay.Interfaces
{
    public interface ICustomerService
    {
        Task<CreateCustomerResponse> AddCustomer(CreateCustomerRequest request);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> UpdateCustomer(int id, UpdateCustomerRequest request);
        Task<Customer?> DeleteCustomer(int id);
    }
}
