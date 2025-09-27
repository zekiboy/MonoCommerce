using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MonoCommerce.WebUI.Models;
using MonoCommerce.Business.Abstract;
using MonoCommerce.Entities;

namespace MonoCommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {

        //test amaçlı create edit kargo kısmını null bırak, sonradan dropdown ile seçilecek şekilde düzenle
        private readonly IMapper _mapper;

        private readonly IProductManager _productManager;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductManager productManager, ILogger<ProductController> logger, IMapper mapper)
        {
            _productManager = productManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Ürün Listesi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productManager.GetAllAsync(); // List<Product>

            // Map Product -> ProductViewModel
             var model = _mapper.Map<List<ProductViewModel>>(products);


            return View(model);
        }

        // GET: Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        
                // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ViewModel -> Entity
              var product = _mapper.Map<Product>(model);

            await _productManager.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ürün Düzenleme Formu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _productManager.GetByIdAsync(id);
                if (product == null) return NotFound();

                // Entity -> ViewModel
                var model = _mapper.Map<ProductViewModel>(product);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün düzenleme formu açılırken hata oluştu. Id: {Id}", id);
                return View("Error");
            }
        }

        // POST: Ürün Düzenleme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                // ViewModel -> Entity
                var product = _mapper.Map<Product>(model);

                await _productManager.UpdateAsync(product);
                _logger.LogInformation("Ürün güncellendi: {ProductId}", product.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncellenirken hata oluştu. Id: {ProductId}", model.Id);
                return View(model);
            }
        }

        // POST: Ürün Silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productManager.DeleteAsync(id);
                _logger.LogInformation("Ürün silindi. Id: {ProductId}", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün silinirken hata oluştu. Id: {ProductId}", id);
                return View("Error");
            }
        }

        // GET: Product/Detail/5
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productManager.GetByIdAsync(id);
            if (product == null) return NotFound();

            // Entity -> ViewModel
            var model = _mapper.Map<ProductViewModel>(product);
            return View(model);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}