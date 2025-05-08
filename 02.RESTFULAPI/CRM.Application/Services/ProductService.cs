using CRM.Core.Entities;
using CRM.Core.Interfaces;

namespace CRM.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
