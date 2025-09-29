using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business.Abstract
{
    public interface IOrderManager
    {
        // CRUD
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);

        // Detaylı metodlar
        Task<Order?> GetOrderWithDetailsAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}