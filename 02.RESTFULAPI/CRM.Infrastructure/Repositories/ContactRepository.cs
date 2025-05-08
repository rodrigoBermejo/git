using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public ContactRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.FindAsync(id) ?? throw new InvalidOperationException("Contact not found");
        }

        public async Task AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            var existingContact = await _context.Contacts.FindAsync(contact.IdContact);
            if (existingContact != null)
            {
                existingContact = contact;
                existingContact.Updated = DateTime.UtcNow;
                existingContact.UpdatedById = _userService.GetCurrentUserId();

                _context.Contacts.Update(existingContact);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Contact not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Contact not found");
            }
        }
    }
}
