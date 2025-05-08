using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController:ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceService.GetAllInvoicesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice?>> GetInvoiceByIdAsync(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(invoice);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceAsync(Invoice invoice)
        {
            await _invoiceService.AddInvoiceAsync(invoice);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvoiceAsync(Invoice invoice)
        {
            await _invoiceService.UpdateInvoiceAsync(invoice);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
