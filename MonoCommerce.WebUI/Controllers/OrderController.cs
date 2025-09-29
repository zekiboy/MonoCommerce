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
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly ISiteManager _siteManager;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderManager orderManager, 
            ISiteManager siteManager, 
            IMapper mapper)
        {
            _orderManager = orderManager;
            _siteManager = siteManager;
            _mapper = mapper;
        }

        // INDEX: Tüm siparişleri detaylarıyla listeler
        public async Task<IActionResult> Index()
        {
            var orders = await _orderManager.GetAllOrdersWithDetailsAsync();
            var model = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
            return View(model);
        }

        // DETAIL: Tek sipariş detay
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderManager.GetOrderWithDetailsAsync(id);
            if (order == null) return NotFound();

            var model = _mapper.Map<OrderViewModel>(order);
            return View(model);
        }

        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // var products = await _productManager.GetAllAsync();
            var sites = await _siteManager.GetAllWithProductsAsync();

            // ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.Sites = new SelectList(sites, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Hatalı alanlar varsa geri dön ve dropdownları tekrar doldur
                var sites = await _siteManager.GetAllAsync();

                // ViewBag.Products = new SelectList(products, "Id", "Name", model.ProductId);
                ViewBag.Sites = new SelectList(sites, "Id", "Name", model.SiteId);
                
                // // ModelState hatalarını kullanıcıya göster
                // TempData["Error"] = "Form hatalı. Lütfen tüm alanları doğru şekilde doldurun.";

                return View(model);
            }

            // Toplam fiyatı hesapla
            model.TotalPrice = model.Quantity * model.UnitPrice;

            var order = _mapper.Map<Order>(model);
            await _orderManager.AddAsync(order);
            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderManager.GetOrderWithDetailsAsync(id);
            if (order == null) return NotFound();

            // var products = await _productManager.GetAllAsync();
            var sites = await _siteManager.GetAllWithProductsAsync();

            // ViewBag.Products = new SelectList(products, "Id", "Name", order.ProductId);
            ViewBag.Sites = new SelectList(sites, "Id", "Name", order.SiteId);

            var model = _mapper.Map<OrderViewModel>(order);
            return View(model);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // var products = await _productManager.GetAllAsync();
                var sites = await _siteManager.GetAllWithProductsAsync();

                // ViewBag.Products = new SelectList(products, "Id", "Name", model.ProductId);
                ViewBag.Sites = new SelectList(sites, "Id", "Name", model.SiteId);
                return View(model);
            }


            // Toplam fiyatı hesapla
            model.TotalPrice = model.Quantity * model.UnitPrice;

            var order = _mapper.Map<Order>(model);
            await _orderManager.UpdateAsync(order);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}