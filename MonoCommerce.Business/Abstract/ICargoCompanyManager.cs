using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business.Abstract
{
    public interface ICargoCompanyManager
    {
        Task<IEnumerable<CargoCompany>> GetAllAsync();
        Task<CargoCompany?> GetByIdAsync(int id);
        Task AddAsync(CargoCompany cargoCompany);
        Task UpdateAsync(CargoCompany cargoCompany);
        Task DeleteAsync(int id);

        // // İleride kargoya özel metotlar buraya eklenebilir
        // Task<IEnumerable<CargoCompany>> GetActiveCargoCompaniesAsync(); 
    }
}