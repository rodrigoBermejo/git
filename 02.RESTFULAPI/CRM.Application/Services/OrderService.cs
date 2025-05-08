using CRM.Core.Entities;
using CRM.Core.Interfaces;

namespace CRM.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
