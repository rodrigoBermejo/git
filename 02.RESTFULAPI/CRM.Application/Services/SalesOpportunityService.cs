using CRM.Core.Entities;
using CRM.Core.Interfaces;

namespace CRM.Application.Services
{
    public class SalesOpportunityService
    {
        private readonly ISalesOpportunityRepository _salesOpportunityRepository;

        public SalesOpportunityService(ISalesOpportunityRepository salesOpportunityRepository)
        {
            _salesOpportunityRepository = salesOpportunityRepository;
        }

        public async Task<IEnumerable<SalesOpportunity>> GetAllSalesOpportunitiesAsync()
        {
            return await _salesOpportunityRepository.GetAllAsync();
        }

        public async Task<SalesOpportunity> GetSalesOpportunityByIdAsync(Guid id)
        {
            return await _salesOpportunityRepository.GetByIdAsync(id);
        }

        public async Task AddSalesOpportunityAsync(SalesOpportunity salesOpportunity)
        {
            await _salesOpportunityRepository.AddAsync(salesOpportunity);
        }

        public async Task UpdateSalesOpportunityAsync(SalesOpportunity salesOpportunity)
        {
            await _salesOpportunityRepository.UpdateAsync(salesOpportunity);
        }

        public async Task DeleteSalesOpportunityAsync(Guid id)
        {
            await _salesOpportunityRepository.DeleteAsync(id);
        }
    }
}
