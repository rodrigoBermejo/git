using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface ISalesOpportunityRepository
    {
        Task<IEnumerable<SalesOpportunity>> GetAllAsync();

        Task<SalesOpportunity> GetByIdAsync(Guid id);

        Task AddAsync(SalesOpportunity salesOportunity);

        Task UpdateAsync(SalesOpportunity salesOportunity);

        Task DeleteAsync(Guid id);
    }
}
