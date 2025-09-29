using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;

namespace MonoCommerce.Data.Concrete
{
    public class CargoCompanyRepository : GenericRepository<CargoCompany>, ICargoCompanyRepository
    {
        public CargoCompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}