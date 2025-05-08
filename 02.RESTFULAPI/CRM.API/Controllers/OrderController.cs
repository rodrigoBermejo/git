using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderService.GetAllOrdersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order?>> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(Order order)
        {
            await _orderService.AddOrderAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync(Order order)
        {
            await _orderService.UpdateOrderAsync(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                await _orderService.DeleteOrderAsync(id);
                return Ok();
            }
        }
    }
}
