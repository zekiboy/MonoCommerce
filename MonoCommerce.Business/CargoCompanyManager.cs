// MonoCommerce.Business/Concrete/CargoCompanyManager.cs
using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;

namespace MonoCommerce.Business
{
    public class CargoCompanyManager : ICargoCompanyManager
    {
        private readonly ICargoCompanyRepository _cargoCompanyRepository;

        public CargoCompanyManager(ICargoCompanyRepository cargoCompanyRepository)
        {
            _cargoCompanyRepository = cargoCompanyRepository;
        }

        public async Task<IEnumerable<CargoCompany>> GetAllAsync()
        {
            return await _cargoCompanyRepository.GetAllAsync();
        }

        public async Task<CargoCompany?> GetByIdAsync(int id)
        {
            return await _cargoCompanyRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CargoCompany entity)
        {
            await _cargoCompanyRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(CargoCompany entity)
        {
            await _cargoCompanyRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _cargoCompanyRepository.DeleteAsync(id);
        }
    }
}