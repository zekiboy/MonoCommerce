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
        private readonly IHtmlExportManager _htmlExportManager; // << ekledik

        public DomainController(IGoDaddyManager goDaddyManager, IHtmlExportManager htmlExportManager)
        {
            _goDaddyManager = goDaddyManager;
            _htmlExportManager = htmlExportManager; // << inject ettik
        }

        // GET: Domain Yönetimi sayfası
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Domain kontrol
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

        // Test amaçlı: domain satın alma (OTE ortamında)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseDomain(string domain)
        {
            if (string.IsNullOrEmpty(domain))
                return BadRequest(new { status = "❌ Domain girilmedi." });

            try
            {
                // DomainPurchaseRequest nesnesini GoDaddy JSON şemasına uygun şekilde oluştur
                var request = new DomainPurchaseRequest
                {
                    Domain = domain,
                    Period = 1,
                    RenewAuto = true,
                    Privacy = false,
                    NameServers = Array.Empty<string>(),
                    Consent = new ConsentInfo
                    {
                        AgreementKeys = new string[] { "DNRA" },
                        AgreedBy = "test@example.com",
                        AgreedAt = DateTime.UtcNow.ToString("o")
                    },
                    ContactAdmin = new ContactInfo(),
                    ContactBilling = new ContactInfo(),
                    ContactRegistrant = new ContactInfo(),
                    ContactTech = new ContactInfo()
                };

                // GoDaddy OTE API isteği
                var success = await _goDaddyManager.PurchaseDomainAsync(request);

                ViewBag.LastDomain = domain;
                ViewBag.IsAvailable = success;

                // Test ortamı mesajı
                ViewBag.Message = success
                    ? $"✅ {domain} için satın alma isteği başarılı (OTE ortamı)."
                    : $"⚠️ {domain} için satın alma isteği gönderildi ama GoDaddy test ortamı reddetti.";

                return View("Index");
            }
            catch (HttpRequestException httpEx)
            {
                // HTTP hatası log ve kullanıcıya göster
                ViewBag.Message = $"🌐 HTTP hatası: {httpEx.Message}";
                ViewBag.IsAvailable = false;
                ViewBag.LastDomain = domain;
                return View("Index");
            }
            catch (Exception ex)
            {
                // Diğer beklenmeyen hatalar
                ViewBag.Message = $"💥 Beklenmeyen hata: {ex.Message}";
                ViewBag.IsAvailable = false;
                ViewBag.LastDomain = domain;
                return View("Index");
            }
        }


        // GET: Domain DNS kayıtlarını göster
        [HttpGet]
        public async Task<IActionResult> DnsRecords(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                ViewBag.Message = "❌ Lütfen bir domain girin.";
                return View("Index");
            }

            try
            {
                var records = await _goDaddyManager.GetDnsRecordsAsync(domain);
                ViewBag.Domain = domain;
                return View(records); // records ViewModel olarak sayfaya gönderilir
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"💥 DNS kayıtları alınamadı: {ex.Message}";
                return View("Index");
            }
        }

        // // POST: DNS kayıtlarını güncelle
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> UpdateDnsRecords(string domain, DnsRecord[] records)
        // {
        //     if (string.IsNullOrWhiteSpace(domain))
        //         return BadRequest("Domain girilmedi.");

        //     try
        //     {
        //         var success = await _goDaddyManager.UpdateDnsRecordsAsync(domain, records);
        //         ViewBag.Message = success
        //             ? $"✅ {domain} için DNS kayıtları başarıyla güncellendi."
        //             : $"⚠️ {domain} için DNS kayıtları güncellenemedi.";

        //         return RedirectToAction("DnsRecords", new { domain });
        //     }
        //     catch (Exception ex)
        //     {
        //         ViewBag.Message = $"💥 DNS güncelleme hatası: {ex.Message}";
        //         return RedirectToAction("DnsRecords", new { domain });
        //     }
        // }

        [HttpPost]
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

// HTML'i local klasöre export et
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExportLocal(string domain)
{
    if (string.IsNullOrWhiteSpace(domain))
        return BadRequest("Domain girilmedi.");

    var htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";

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

// HTML'i sunucuya deploy et (simülasyon)
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ExportServer(string domain)
{
    if (string.IsNullOrWhiteSpace(domain))
        return BadRequest("Domain girilmedi.");

    var htmlContent = "<html><body><h1>MonoCommerce Export Test</h1></body></html>";

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