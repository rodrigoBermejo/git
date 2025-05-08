using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();

        Task<Contact> GetByIdAsync(Guid id);

        Task AddAsync(Contact contact);

        Task UpdateAsync(Contact contact);

        Task DeleteAsync(Guid id);
    }
}
