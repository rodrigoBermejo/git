using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface ISupportCaseRepository
    {
        Task<IEnumerable<SupportCase>> GetAllAsync();

        Task<SupportCase> GetByIdAsync(Guid id);

        Task AddAsync(SupportCase supportCase);

        Task UpdateAsync(SupportCase supportCase);

        Task DeleteAsync(Guid id);
    }
}
