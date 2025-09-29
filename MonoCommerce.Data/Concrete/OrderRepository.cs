using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;

namespace MonoCommerce.Data.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Tek bir siparişi detaylarıyla getir
        public async Task<Order?> GetOrderWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Site)    // site bilgisi
                .FirstOrDefaultAsync(o => o.Id == id); // id'ye göre filtrele
        }

        // Tüm siparişleri detaylarıyla getir
        public async Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync()
        {
            return await _context.Orders
                .Include(o => o.Site)
                .ToListAsync();
        }
    }
}