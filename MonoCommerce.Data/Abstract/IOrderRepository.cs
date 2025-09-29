using System.Collections.Generic;
using MonoCommerce.Entities;

namespace MonoCommerce.Data.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {

        // Detaylı sipariş metodları
        Task<Order?> GetOrderWithDetailsAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}