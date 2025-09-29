// MonoCommerce.WebUI/Controllers/CargoCompanyController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonoCommerce.Business.Abstract;
using MonoCommerce.WebUI.Models;
using MonoCommerce.Entities;

namespace MonoCommerce.WebUI.Controllers
{
    public class CargoCompanyController : Controller
    {
        private readonly ICargoCompanyManager _cargoCompanyManager;
        private readonly IMapper _mapper;

        public CargoCompanyController(ICargoCompanyManager cargoCompanyManager, IMapper mapper)
        {
            _cargoCompanyManager = cargoCompanyManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cargoCompanies = await _cargoCompanyManager.GetAllAsync();
            var model = _mapper.Map<IEnumerable<CargoCompanyViewModel>>(cargoCompanies);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cargoCompany = await _cargoCompanyManager.GetByIdAsync(id);
            if (cargoCompany == null) return NotFound();

            var model = _mapper.Map<CargoCompanyViewModel>(cargoCompany);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CargoCompanyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cargoCompany = _mapper.Map<CargoCompany>(model);
            await _cargoCompanyManager.AddAsync(cargoCompany);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cargoCompany = await _cargoCompanyManager.GetByIdAsync(id);
            if (cargoCompany == null) return NotFound();

            var model = _mapper.Map<CargoCompanyViewModel>(cargoCompany);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CargoCompanyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cargoCompany = _mapper.Map<CargoCompany>(model);
            await _cargoCompanyManager.UpdateAsync(cargoCompany);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _cargoCompanyManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}