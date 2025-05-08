using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class SupportCaseRepository : ISupportCaseRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public SupportCaseRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<SupportCase>> GetAllAsync()
        {
            return await _context.SupportCases.ToListAsync();
        }

        public async Task<SupportCase> GetByIdAsync(Guid id)
        {
            return await _context.SupportCases.FindAsync(id) ?? throw new InvalidOperationException("Support Case not found");
        }

        public async Task AddAsync(SupportCase supportCase)
        {
            var newSupportCase = supportCase;
            newSupportCase.Created = DateTime.UtcNow;
            newSupportCase.CreatedById = _userService.GetCurrentUserId();
            _context.SupportCases.Add(newSupportCase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SupportCase supportCase)
        {
            var existingSupportCase = await _context.SupportCases.FindAsync(supportCase.IdSupportCase);
            if (existingSupportCase != null)
            {
                existingSupportCase = supportCase;
                existingSupportCase.Updated = DateTime.UtcNow;
                existingSupportCase.UpdatedById = _userService.GetCurrentUserId();
                _context.SupportCases.Update(existingSupportCase);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Support Case not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var supportCase = await _context.SupportCases.FindAsync(id);
            if (supportCase != null)
            {
                _context.SupportCases.Remove(supportCase);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Support Case not found");
            }
        }
    }

}
