using CRM.Core.Entities;
using CRM.Core.Interfaces;

namespace CRM.Application.Services
{
    public class SupportCaseService
    {
        private readonly ISupportCaseRepository _supportCaseRepository;

        public SupportCaseService(ISupportCaseRepository supportCaseRepository)
        {
            _supportCaseRepository = supportCaseRepository;
        }

        public async Task<IEnumerable<SupportCase>> GetAllSupportCasesAsync()
        {
            return await _supportCaseRepository.GetAllAsync();
        }

        public async Task<SupportCase> GetSupportCaseByIdAsync(Guid id)
        {
            return await _supportCaseRepository.GetByIdAsync(id);
        }

        public async Task AddSupportCaseAsync(SupportCase supportCase)
        {
            await _supportCaseRepository.AddAsync(supportCase);
        }

        public async Task UpdateSupportCaseAsync(SupportCase supportCase)
        {
            await _supportCaseRepository.UpdateAsync(supportCase);
        }

        public async Task DeleteSupportCaseAsync(Guid id)
        {
            await _supportCaseRepository.DeleteAsync(id);
        }
    }
}
