using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business
{
    public class ProductManager : IProductManager
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductManager(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _productRepo.GetAllAsync();
        public Task<Product> GetByIdAsync(int id) => _productRepo.GetByIdAsync(id);
        public Task AddAsync(Product product) => _productRepo.AddAsync(product);
        public Task UpdateAsync(Product product) => _productRepo.UpdateAsync(product);
        public Task DeleteAsync(int id) => _productRepo.DeleteAsync(id);
    }
}