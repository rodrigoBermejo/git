using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class SalesOpportunityRepository : ISalesOpportunityRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public SalesOpportunityRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<SalesOpportunity>> GetAllAsync()
        {
            return await _context.SalesOpportunities.ToListAsync();
        }
        
        public async Task<SalesOpportunity> GetByIdAsync(Guid id)
        {
            return await _context.SalesOpportunities.FindAsync(id) ?? throw new InvalidOperationException("Sales Opportunity not found");
        }
        
        public async Task AddAsync(SalesOpportunity salesOpportunity)
        {
            var newSalesOpportunity = salesOpportunity;
            newSalesOpportunity.Created = DateTime.UtcNow;
            newSalesOpportunity.CreatedById = _userService.GetCurrentUserId();
            _context.SalesOpportunities.Add(newSalesOpportunity);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(SalesOpportunity salesOpportunity)
        {
            var existingSalesOpportunity = await _context.SalesOpportunities.FindAsync(salesOpportunity.IdSalesOpportunity);
            if (existingSalesOpportunity != null)
            {
                existingSalesOpportunity = salesOpportunity;
                existingSalesOpportunity.Updated = DateTime.UtcNow;
                existingSalesOpportunity.UpdatedById = _userService.GetCurrentUserId();
                _context.SalesOpportunities.Update(existingSalesOpportunity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Sales Opportunity not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var salesOpportunity = await _context.SalesOpportunities.FindAsync(id);
            if (salesOpportunity != null)
            {
                _context.SalesOpportunities.Remove(salesOpportunity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Sales Opportunity not found");
            }
        }
    }
}
