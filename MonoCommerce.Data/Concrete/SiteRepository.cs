using Microsoft.EntityFrameworkCore;
using MonoCommerce.Entities;
using MonoCommerce.Data.Abstract;

namespace MonoCommerce.Data.Concrete
{
    public class SiteRepository : GenericRepository<Site>, ISiteRepository
    {
        private readonly AppDbContext _context;

        public SiteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Site?> GetSiteWithProductAsync(int id)
        {
            return await _context.Sites
                                 .Include(s => s.Product)  // Product ile ilişkiyi getir
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Site>> GetAllWithProductsAsync()
        {
            return await _context.Sites
                                 .Include(s => s.Product) // Product dahil
                                 .ToListAsync();
        }
    }
}