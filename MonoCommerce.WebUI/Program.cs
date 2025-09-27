using Microsoft.EntityFrameworkCore;
using MonoCommerce.Business.Abstract;
using MonoCommerce.Business; 
using MonoCommerce.Data.Abstract;
using MonoCommerce.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext kaydı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=monocommerce.db"));
    
// AutoMapper kaydı
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Repository & Business katmanları
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductManager, ProductManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();