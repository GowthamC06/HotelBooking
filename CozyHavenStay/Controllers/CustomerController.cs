using Microsoft.AspNetCore.Mvc;
using CozyHavenStay.Interfaces;
using CozyHavenStay.Models;
using CozyHavenStay.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CozyHavenStay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CreateCustomerResponse>> Register(CreateCustomerRequest request)
        {
            var result = await _customerService.AddCustomer(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<Customer>> Update(int id, UpdateCustomerRequest request)
        {
            var updated = await _customerService.UpdateCustomer(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var deleted = await _customerService.DeleteCustomer(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
