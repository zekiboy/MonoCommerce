using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business
{
    public class OrderManager : IOrderManager
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IOrderRepository _orderRepo;

        public OrderManager(IGenericRepository<Order> orderRepository, IOrderRepository orderRepo)
        {
            _orderRepository = orderRepository;
            _orderRepo = orderRepo;
        }

        // CRUD
        public Task<IEnumerable<Order>> GetAllAsync() => _orderRepository.GetAllAsync();
        public Task<Order?> GetByIdAsync(int id) => _orderRepository.GetByIdAsync(id);
        public Task AddAsync(Order order) => _orderRepository.AddAsync(order);
        public Task UpdateAsync(Order order) => _orderRepository.UpdateAsync(order);
        public Task DeleteAsync(int id) => _orderRepository.DeleteAsync(id);

        // Detaylı sipariş metodları
        public Task<Order?> GetOrderWithDetailsAsync(int id) => _orderRepo.GetOrderWithDetailsAsync(id);
        public Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync() => _orderRepo.GetAllOrdersWithDetailsAsync();
    }
}