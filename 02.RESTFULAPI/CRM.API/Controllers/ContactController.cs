using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController:ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactService.GetAllContactsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact?>> GetContactByIdAsync(Guid id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(contact);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContactAsync(Contact contact)
        {
            await _contactService.AddContactAsync(contact);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContactAsync(Contact contact)
        {
            await _contactService.UpdateContactAsync(contact);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactAsync(Guid id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                await _contactService.DeleteContactAsync(id);
                return Ok();
            }
        }
    }
}
