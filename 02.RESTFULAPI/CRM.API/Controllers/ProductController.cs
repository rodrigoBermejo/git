using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProductByIdAsync(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            await _productService.AddProductAsync(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            await _productService.UpdateProductAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                await _productService.DeleteProductAsync(id);
                return Ok();
            }
        }
    }
}
