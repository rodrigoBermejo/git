using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public ProductRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id) ?? throw new InvalidOperationException("Products not found");
        }

        public async Task AddAsync(Product product)
        {
            var newProduct = product;
            newProduct.Created = DateTime.UtcNow;
            newProduct.CreatedById = _userService.GetCurrentUserId();
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.IdProduct);
            if (existingProduct != null)
            {
                existingProduct = product;
                existingProduct.Updated = DateTime.UtcNow;
                existingProduct.UpdatedById = _userService.GetCurrentUserId();
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Product not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Product not found");
            }
        }
    }
}
