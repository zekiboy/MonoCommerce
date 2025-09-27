using MonoCommerce.Entities;

namespace MonoCommerce.Data.Abstract
{
    public interface ISiteRepository : IGenericRepository<Site>
    {
        // ID ile Site ve Product'ı dahil ederek getir
        Task<Site?> GetSiteWithProductAsync(int id);

        // Tüm siteleri Product dahil olarak getir
        Task<IEnumerable<Site>> GetAllWithProductsAsync();
    }
}