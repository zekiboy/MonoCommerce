using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business.Abstract
{
    public interface ISiteManager
    {
        Task<IEnumerable<Site>> GetAllAsync();
        Task<Site?> GetByIdAsync(int id);
        Task AddAsync(Site site);
        Task UpdateAsync(Site site);
        Task DeleteAsync(int id);

        // Product dahil getirme metodları
        Task<Site?> GetSiteWithProductAsync(int id);
        Task<IEnumerable<Site>> GetAllWithProductsAsync();
    }
}