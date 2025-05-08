using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public OrderRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id) ?? throw new InvalidOperationException("Order not found");
        }
        public async Task AddAsync(Order order)
        {
            var newOrder = order;
            newOrder.Created = DateTime.UtcNow;
            newOrder.CreatedById = _userService.GetCurrentUserId();
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.IdOrder);
            if (existingOrder != null)
            {
                existingOrder = order;
                existingOrder.Updated = DateTime.UtcNow;
                existingOrder.UpdatedById = _userService.GetCurrentUserId();
                _context.Orders.Update(existingOrder);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Order not found");
            }
        }

    }
}
