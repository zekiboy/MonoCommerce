using Microsoft.EntityFrameworkCore;
using MonoCommerce.Entities;

namespace MonoCommerce.Data.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //dotnet ef migrations add InitialCreate MonoCommerce.Data içerisinde migration oluştur
        //dotnet ef database update ile veritabanını oluştur WebUI katmanında çalıştır

        public DbSet<Product> Products { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<CargoCompany> CargoCompanies { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}