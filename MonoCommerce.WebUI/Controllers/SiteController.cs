using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonoCommerce.Business.Abstract;
using MonoCommerce.Entities;
using MonoCommerce.Entities.Domain;
using MonoCommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MonoCommerce.WebUI.Controllers
{
    public class SiteController : Controller
    {
        private readonly ISiteManager _siteManager;
        private readonly IProductManager _productManager;
        private readonly IGoDaddyManager _goDaddyManager;
        private readonly IHtmlExportManager _htmlExportManager;
        private readonly IMapper _mapper;

        public SiteController(
            ISiteManager siteManager,
            IProductManager productManager,
            IGoDaddyManager goDaddyManager,
            IHtmlExportManager htmlExportManager,
            IMapper mapper)
        {
            _siteManager = siteManager;
            _productManager = productManager;
            _goDaddyManager = goDaddyManager;
            _htmlExportManager = htmlExportManager;
            _mapper = mapper;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var sites = await _siteManager.GetAllWithProductsAsync();
            var model = _mapper.Map<IEnumerable<SiteViewModel>>(sites);
            return View(model);
        }

        // CREATE GET
        [HttpGet]
        public async Task<IActionResult> Create(string lastDomain = null)
        {
            // TempData mesajı varsa ViewBag'e aktar
            if (TempData["DomainMessage"] != null)
                ViewBag.DomainMessage = TempData["DomainMessage"].ToString();

            // Son domaini ViewBag'e aktar
            ViewBag.LastDomain = lastDomain;

            // Ürünleri al
            var products = await _productManager.GetAllAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");

            // Test içerik
            var model = new SiteViewModel
            {
                Url = lastDomain ?? "testdomain.xyz",
                HtmlContent = "<h1>Test HTML Content</h1><p>Bu içerik test amaçlıdır.</p>",
                FormHtml = "<form>Test form export</form>"
            };

            return View(model);
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

            TempData["DomainMessage"] = "✅ Site başarıyla kaydedildi.";
            return RedirectToAction(nameof(Create));
        }

        // ✅ DOMAIN KONTROL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckDomain(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                TempData["DomainMessage"] = "❌ Lütfen bir domain girin.";
                return RedirectToAction("Create");
            }

            bool isAvailable = await _goDaddyManager.IsDomainAvailableAsync(url);
            TempData["DomainMessage"] = isAvailable
                ? $"✅ {url} alınabilir."
                : $"❌ {url} zaten alınmış veya geçersiz.";

            // Son domaini query parametresi ile Create action'a gönder
            return RedirectToAction("Create", new { lastDomain = url });
        }

        // // CREATE GET
        // [HttpGet]
        // public async Task<IActionResult> Create()
        // {
        //     if (TempData["DomainMessage"] != null)
        //         ViewBag.DomainMessage = TempData["DomainMessage"].ToString();

        //     var products = await _productManager.GetAllAsync();
        //     ViewBag.Products = new SelectList(products, "Id", "Name");

        //     // Test içerik
        //     var model = new SiteViewModel
        //     {
        //         Url = "testdomain.xyz",
        //         HtmlContent = "<h1>Test HTML Content</h1><p>Bu içerik test amaçlıdır.</p>",
        //         FormHtml = "<form>Test form export</form>"
        //     };

        //     return View(model);
        // }

        // // CREATE POST
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(SiteViewModel model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         var products = await _productManager.GetAllAsync();
        //         ViewBag.Products = new SelectList(products, "Id", "Name", model.ProductId);
        //         return View(model);
        //     }

        //     var site = _mapper.Map<Site>(model);
        //     await _siteManager.AddAsync(site);

        //     TempData["DomainMessage"] = "✅ Site başarıyla kaydedildi.";
        //     return RedirectToAction(nameof(Create));
        // }

        // // ✅ DOMAIN KONTROL
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> CheckDomain(string url)
        // {
        //     if (string.IsNullOrWhiteSpace(url))
        //     {
        //         TempData["DomainMessage"] = "❌ Lütfen bir domain girin.";
        //         return RedirectToAction("Create");
        //     }

        //     bool isAvailable = await _goDaddyManager.IsDomainAvailableAsync(url);
        //     TempData["DomainMessage"] = isAvailable
        //         ? $"✅ {url} alınabilir."
        //         : $"❌ {url} zaten alınmış veya geçersiz.";

        //     return RedirectToAction("Create");
        // }

// ✅ DOMAIN SATIN ALMA (Simülasyon)
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult PurchaseDomain(string url)
{
    if (string.IsNullOrWhiteSpace(url))
    {
        TempData["DomainMessage"] = "❌ Domain girilmedi.";
        return RedirectToAction("Create");
    }

    bool success = true;
    TempData["DomainMessage"] = success
        ? $"✅ {url} için satın alma simulate başarılı."
        : $"⚠️ {url} için satın alma simulate başarısız.";

    // Son domaini query parametresi ile Create action'a gönder
    return RedirectToAction("Create", new { lastDomain = url });
}

// ✅ DNS SIMULASYON
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> UpdateDnsTest(string url)
{
    if (string.IsNullOrWhiteSpace(url))
    {
        TempData["DomainMessage"] = "❌ Domain girilmedi.";
        return RedirectToAction("Create");
    }

    var request = new DnsUpdateRequest
    {
        Domain = url,
        Records = new[]
        {
            new DnsRecord { Name = "www", Type = "A", Data = "127.0.0.1", Ttl = 3600 }
        }
    };

    var success = await _goDaddyManager.UpdateDnsAsync(request, simulate: true);

    TempData["DomainMessage"] = success
        ? $"✅ DNS update simulate başarılı. Domain: {url}"
        : $"⚠️ DNS update simulate başarısız. Domain: {url}";

    return RedirectToAction("Create", new { lastDomain = url });
}

// ✅ HTML EXPORT LOCAL
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExportLocal(SiteViewModel model)
{
    if (string.IsNullOrWhiteSpace(model.Url))
    {
        TempData["DomainMessage"] = "❌ Site URL girilmedi.";
        return RedirectToAction("Create");
    }

    string htmlContent;
    if (System.IO.File.Exists("./Templates/SiteTemplate.html"))
    {
        string template = await System.IO.File.ReadAllTextAsync("./Templates/SiteTemplate.html");
        htmlContent = template
            .Replace("((siteName))", model.Name)
            .Replace("((description))", model.Description ?? "")
            .Replace("((imageUrl))", model.ImageUrl ?? "")
            .Replace("((price))", model.Prices ?? "")
            .Replace("((paymentMethod))", model.PaymentMethod ?? "")
            .Replace("((currency))", model.Currency ?? "")
            .Replace("((whatsapp))", model.WhatsappPhone ?? "")
            .Replace("((pixelCode))", model.PixelCode ?? "")
            .Replace("((formHtml))", model.FormHtml ?? "")
            .Replace("((url))", model.Url);
    }
    else
    {
        htmlContent = model.HtmlContent ??
            "<html><body><h1>MonoCommerce Export Test</h1><p>Varsayılan içerik.</p></body></html>";
    }

    var request = new HtmlExportRequest
    {
        Domain = model.Url,
        HtmlContent = htmlContent,
        TargetPath = $"./Exports/{model.Url}.html"
    };

    var success = await _htmlExportManager.ExportToLocalAsync(request);

    TempData["DomainMessage"] = success
        ? $"✅ HTML local export başarılı. {request.TargetPath}"
        : $"⚠️ HTML export başarısız.";

    // Son domaini query parametresi ile Create action'a gönder
    return RedirectToAction("Create", new { lastDomain = model.Url });
}

// ✅ HTML EXPORT SERVER
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExportServer(string url, string htmlContent)
{
    if (string.IsNullOrWhiteSpace(url))
    {
        TempData["DomainMessage"] = "❌ Domain girilmedi.";
        return RedirectToAction("Create");
    }

    if (string.IsNullOrWhiteSpace(htmlContent))
        htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";

    var request = new HtmlExportRequest
    {
        Domain = url,
        HtmlContent = htmlContent
    };

    var success = await _htmlExportManager.ExportToServerAsync(request);

    TempData["DomainMessage"] = success
        ? $"✅ HTML server export simulate başarılı. Domain: {url}"
        : $"⚠️ HTML server export simulate başarısız. Domain: {url}";

    // Son domaini query parametresi ile Create action'a gönder
    return RedirectToAction("Create", new { lastDomain = url });
}


    }
}