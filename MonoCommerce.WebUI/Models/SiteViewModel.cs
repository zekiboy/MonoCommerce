using System.ComponentModel.DataAnnotations;

namespace MonoCommerce.WebUI.Models
{
    public class SiteViewModel
    {
        public int Id { get; set; }  // BaseEntity’den gelir

        // Zorunlu alanlar
        [Required(ErrorMessage = "Lütfen geçerli bir site adı girin.")]
        [Display(Name = "Site Adı")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Lütfen geçerli bir URL girin.")]
        [Display(Name = "Site URL")]
        public string Url { get; set; } = null!;

        [Display(Name = "Site Resim URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        // Site - Ürün ilişkisi
        [Required(ErrorMessage = "Lütfen bir ürün seçin.")]
        [Display(Name = "Ürün")]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        public string? ProductName { get; set; } // Opsiyonel gösterim için

        // Ödeme ve fiyatlandırma
        [Display(Name = "Fiyat Bilgisi")]
        public string? Prices { get; set; }

        [Display(Name = "Ödeme Yöntemi")]
        public string PaymentMethod { get; set; } = "Kapıda Ödeme";

        [Display(Name = "Para Birimi")]
        public string Currency { get; set; } = "TRY";

        // İletişim
        [Display(Name = "Whatsapp Aktif mi?")]
        public bool WhatsappEnabled { get; set; }

        [Display(Name = "Whatsapp Kullanım Zamanı")]
        public string? WhatsappAvailableTime { get; set; }

        [Display(Name = "Whatsapp Telefon")]
        public string? WhatsappPhone { get; set; }

        [Display(Name = "SMS Onayı")]
        public bool SmsApproval { get; set; }

        // Teknik alanlar
        [Display(Name = "FTP Oluşturuldu mu?")]
        public bool FtpCreated { get; set; }

        [Display(Name = "Tema")]
        public string Theme { get; set; } = "Yaprak Site";

        // Pixel & Tracking
        [Display(Name = "Pixel Kodu")]
        public string? PixelCode { get; set; }

        [Display(Name = "Özel Pixel Kodu")]
        public string? PixelSpecialCode { get; set; }

        [Display(Name = "Pixel ID'leri")]
        public string? PixelIds { get; set; }

        [Display(Name = "Counter Kodu")]
        public string? CounterCode { get; set; }

        // HTML içerikler
        [Display(Name = "HTML İçerik")]
        public string? HtmlContent { get; set; }

        [Display(Name = "Form HTML")]
        public string? FormHtml { get; set; }

        // DNS ve HTML export test alanları
        [Display(Name = "Test Domain")]
        public string? TestDomain { get; set; }

        [Display(Name = "Export HTML İçeriği")]
        public string? ExportHtmlContent { get; set; }
    }
}