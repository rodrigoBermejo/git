using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer?>> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customer);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(Customer customer)
        {
            await _customerService.AddCustomerAsync(customer);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerAsync(Customer customer)
        {
            await _customerService.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                await _customerService.DeleteCustomerAsync(id);
                return Ok();
            }
        }
    }
}
