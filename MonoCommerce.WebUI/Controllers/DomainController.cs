using Microsoft.AspNetCore.Mvc;
using MonoCommerce.Business.Abstract;
using MonoCommerce.Entities.Domain;
using System;
using System.Threading.Tasks;

namespace MonoCommerce.WebUI.Controllers
{
    public class DomainController : Controller
    {
        private readonly IGoDaddyManager _goDaddyManager;
        private readonly IHtmlExportManager _htmlExportManager; // inject edildi

        public DomainController(IGoDaddyManager goDaddyManager, IHtmlExportManager htmlExportManager)
        {
            _goDaddyManager = goDaddyManager;
            _htmlExportManager = htmlExportManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Domain kontrol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckDomain(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                ViewBag.Message = "❌ Lütfen bir domain girin.";
                ViewBag.IsAvailable = false;
                return View("Index");
            }

            bool isAvailable = await _goDaddyManager.IsDomainAvailableAsync(domain);

            ViewBag.Message = isAvailable
                ? $"{domain} alınabilir."
                : $"{domain} zaten alınmış veya geçersiz.";
            ViewBag.IsAvailable = isAvailable;
            ViewBag.LastDomain = domain;

            return View("Index");
        }

        // Domain satın alma (test)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseDomain(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                return BadRequest("Domain girilmedi.");

            // Test ortamı simulate
            bool success = true;

            ViewBag.LastDomain = domain;
            ViewBag.IsAvailable = success;
            ViewBag.Message = success
                ? $"✅ {domain} için satın alma simulate başarılı"
                : $"⚠️ {domain} için satın alma simulate başarısız";

            return View("Index");
        }

        // DNS simülasyon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDnsTest(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
                return BadRequest("Domain girilmedi.");

            var request = new DnsUpdateRequest
            {
                Domain = domain,
                Records = new[]
                {
                    new DnsRecord { Name = "www", Type = "A", Data = "127.0.0.1", Ttl = 3600 }
                }
            };

            var success = await _goDaddyManager.UpdateDnsAsync(request, simulate: true);

            ViewBag.Message = success
                ? $"✅ DNS update simulate başarılı. Domain: {domain}"
                : $"⚠️ DNS update simulate başarısız. Domain: {domain}";

            return View("Index");
        }


// HTML export local (SiteViewModel verileriyle)
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExportLocal(string domain, string htmlContent)
{
    if (string.IsNullOrWhiteSpace(domain))
        return BadRequest("Domain girilmedi.");

    // TempData veya Session üzerinden SiteViewModel alınır
    // Eğer direkt formdan gönderemiyorsak, Create aksiyonunda SiteViewModel’i TempData’da tutabiliriz
    var siteModelJson = TempData["SiteModel"] as string;
    MonoCommerce.WebUI.Models.SiteViewModel? siteModel = null;

    if (!string.IsNullOrEmpty(siteModelJson))
        siteModel = System.Text.Json.JsonSerializer.Deserialize<MonoCommerce.WebUI.Models.SiteViewModel>(siteModelJson);

    // Eğer siteModel yoksa sadece htmlContent kullanılır
    if (siteModel == null)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";
    }
    else
    {
        // Dinamik HTML oluştur
        htmlContent = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{siteModel.Name}</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; background-color: #f9f9f9; }}
        .container {{ max-width: 800px; margin: auto; background: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1); }}
        h1 {{ color: #333; }}
        .price {{ color: #2a9d8f; font-size: 1.5em; }}
        .product-image {{ max-width: 100%; border-radius: 8px; }}
        .info {{ margin-top: 20px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>{siteModel.Name}</h1>
        {(string.IsNullOrWhiteSpace(siteModel.ImageUrl) ? "" : $"<img src='{siteModel.ImageUrl}' alt='Site Görseli' class='product-image' />")}
        <p>{siteModel.Description}</p>
        <div class='info'>
            <p><strong>Ürün:</strong> {siteModel.ProductName}</p>
            <p class='price'><strong>Fiyat:</strong> {siteModel.Prices} {siteModel.Currency}</p>
            <p><strong>Ödeme Yöntemi:</strong> {siteModel.PaymentMethod}</p>
            <p><strong>WhatsApp:</strong> {(siteModel.WhatsappEnabled ? siteModel.WhatsappPhone : "Aktif değil")}</p>
            <p><strong>Pixel Code:</strong> {siteModel.PixelCode}</p>
        </div>
        <hr />
        {(string.IsNullOrWhiteSpace(siteModel.HtmlContent) ? "" : siteModel.HtmlContent)}
        {(string.IsNullOrWhiteSpace(siteModel.FormHtml) ? "" : siteModel.FormHtml)}
    </div>
</body>
</html>";
    }

    // Export işlemi
    var request = new HtmlExportRequest
    {
        Domain = domain,
        HtmlContent = htmlContent,
        TargetPath = $"./Exports/{domain}.html"
    };

    var success = await _htmlExportManager.ExportToLocalAsync(request);

    ViewBag.Message = success
        ? $"✅ HTML local export başarılı. Path: {request.TargetPath}"
        : $"⚠️ HTML export başarısız.";

    return View("Index");
}

        // // HTML export local
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> ExportLocal(string domain, string htmlContent)
        // {
        //     if (string.IsNullOrWhiteSpace(domain))
        //         return BadRequest("Domain girilmedi.");

        //     if (string.IsNullOrWhiteSpace(htmlContent))
        //         htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";

        //     var request = new HtmlExportRequest
        //     {
        //         Domain = domain,
        //         HtmlContent = htmlContent,
        //         TargetPath = $"./Exports/{domain}.html"
        //     };

        //     var success = await _htmlExportManager.ExportToLocalAsync(request);

        //     ViewBag.Message = success
        //         ? $"✅ HTML local export başarılı. Path: {request.TargetPath}"
        //         : $"⚠️ HTML export başarısız.";

        //     return View("Index");
        // }

        // HTML export server (simulate)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportServer(string domain, string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(domain))
                return BadRequest("Domain girilmedi.");

            if (string.IsNullOrWhiteSpace(htmlContent))
                htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";

            var request = new HtmlExportRequest
            {
                Domain = domain,
                HtmlContent = htmlContent
            };

            var success = await _htmlExportManager.ExportToServerAsync(request);

            ViewBag.Message = success
                ? $"✅ HTML server export simulate başarılı. Domain: {domain}"
                : $"⚠️ HTML server export simulate başarısız.";

            return View("Index");
        }
    }
}