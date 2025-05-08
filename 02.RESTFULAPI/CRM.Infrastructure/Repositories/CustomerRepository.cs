using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public CustomerRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id) ?? throw new InvalidOperationException("Customer not found");
        }

        public async Task AddAsync(Customer customer)
        {
            var newCustomer = customer;
            newCustomer.Created = DateTime.UtcNow;
            newCustomer.CreatedById = _userService.GetCurrentUserId();
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.IdCustomer);
                if (existingCustomer != null)
                {
                    existingCustomer = customer;
                    existingCustomer.Updated = DateTime.UtcNow;
                    existingCustomer.UpdatedById = _userService.GetCurrentUserId();
                    _context.Customers.Update(existingCustomer);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException("Customer not found");
                }
            }
            catch (Exception ex) {
                throw new Exception("New Exception", ex.InnerException);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Customer not found");
            }
        }
    }
}
