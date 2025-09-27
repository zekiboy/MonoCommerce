using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonoCommerce.Business.Abstract;
using MonoCommerce.WebUI.Models;
using MonoCommerce.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MonoCommerce.WebUI.Controllers
{
    public class SiteController : Controller
    {
        private readonly ISiteManager _siteManager;
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public SiteController(ISiteManager siteManager, IMapper mapper, IProductManager productManager)
        {
            _siteManager = siteManager;
            _mapper = mapper;
            _productManager = productManager;
        }

        // INDEX: Tüm siteleri listeler
        public async Task<IActionResult> Index()
        {
            var sites = await _siteManager.GetAllWithProductsAsync();
            var model = _mapper.Map<IEnumerable<SiteViewModel>>(sites);
            return View(model);
        }

        // DETAIL: Tek site detay
        public async Task<IActionResult> Details(int id)
        {
            var site = await _siteManager.GetSiteWithProductAsync(id);
            if (site == null) return NotFound();

            var model = _mapper.Map<SiteViewModel>(site);
            return View(model);
        }

        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await _productManager.GetAllAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SiteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var products = await _productManager.GetAllAsync();
                ViewBag.Products = new SelectList(products, "Id", "Name", model.ProductId);
                return View(model);
            }

            var site = _mapper.Map<Site>(model);
            await _siteManager.AddAsync(site);
            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var site = await _siteManager.GetSiteWithProductAsync(id); // Product dahil çek
            if (site == null) return NotFound();

            var products = await _productManager.GetAllAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name", site.ProductId);

            var model = _mapper.Map<SiteViewModel>(site);
            return View(model);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SiteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var products = await _productManager.GetAllAsync();
                ViewBag.Products = new SelectList(products, "Id", "Name", model.ProductId);
                return View(model);
            }

            var site = _mapper.Map<Site>(model);
            await _siteManager.UpdateAsync(site);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _siteManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}