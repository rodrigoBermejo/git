using CRM.Core.Entities;
using CRM.Core.Interfaces;

namespace CRM.Application.Services
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task AddContactAsync(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactRepository.UpdateAsync(contact);
        }

        public async Task DeleteContactAsync(Guid id)
        {
            await _contactRepository.DeleteAsync(id);
        }
    }
}
