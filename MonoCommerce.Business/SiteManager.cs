using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoCommerce.Business
{
    public class SiteManager : ISiteManager
    {
        private readonly IGenericRepository<Site> _siteRepository;
        private readonly ISiteRepository _siteRepo;


        public SiteManager(IGenericRepository<Site> siteRepository, ISiteRepository siteRepo)
        {
            _siteRepository = siteRepository;
            _siteRepo = siteRepo;
        }

        //CRUD
        public Task<IEnumerable<Site>> GetAllAsync() => _siteRepository.GetAllAsync();
        public Task<Site?> GetByIdAsync(int id) => _siteRepository.GetByIdAsync(id);
        public Task AddAsync(Site site) => _siteRepository.AddAsync(site);
        public Task UpdateAsync(Site site) => _siteRepository.UpdateAsync(site);

        public Task DeleteAsync(int id) => _siteRepository.DeleteAsync(id);

        // Product dahil
        public Task<Site?> GetSiteWithProductAsync(int id) => _siteRepo.GetSiteWithProductAsync(id);
        public Task<IEnumerable<Site>> GetAllWithProductsAsync() => _siteRepo.GetAllWithProductsAsync();


    }
}