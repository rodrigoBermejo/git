using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public InvoiceRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.ToListAsync();
        }
        
        public async Task<Invoice> GetByIdAsync(Guid id)
        {
            return await _context.Invoices.FindAsync(id) ?? throw new InvalidOperationException("Invoice not found");
        }
        
        public async Task AddAsync(Invoice invoice)
        {
            var newInvoice = invoice;
            newInvoice.Created = DateTime.UtcNow;
            newInvoice.CreatedById = _userService.GetCurrentUserId();
            _context.Invoices.Add(newInvoice);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices.FindAsync(invoice.IdInvoice);
            if (existingInvoice != null) {
                existingInvoice = invoice;
                existingInvoice.Updated = DateTime.UtcNow;
                existingInvoice.UpdatedById = _userService.GetCurrentUserId();
                _context.Invoices.Update(existingInvoice);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Invoice not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Invoice not found");
            }
        }
    }
}
