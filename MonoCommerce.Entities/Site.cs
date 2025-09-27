namespace MonoCommerce.Entities
{
    public class Site : BaseEntity
    {

        // Zorunlu alanlar
        public string Name { get; set; } = null!;             // Site Adı
        public string Url { get; set; } = null!;              // Site Url
        public string? ImageUrl { get; set; }                 // Site Resim URL
        public string? Description { get; set; }              // Site Açıklaması
        public bool IsActive { get; set; }                    // Site Durumu (Aktif / Pasif)

        // Site - Ürün ilişkisi
        public int ProductId { get; set; }                    // Site Ürünü (FK)
        public Product Product { get; set; } = null!;

        // Ödeme ve fiyatlandırma
        public string? Prices { get; set; }                   // Site Fiyatları (opsiyonel açıklama/JSON olabilir)
        public string PaymentMethod { get; set; } = "Kapıda Ödeme";  // Site Ödeme Method
        public string Currency { get; set; } = "TRY";         // Site Kuru (Türk Lirası vs.)

        // İletişim
        public bool WhatsappEnabled { get; set; }             // Site Whatsapp (Hayır/Evet)
        public string? WhatsappAvailableTime { get; set; }    // Whatsapp Zamanı
        public string? WhatsappPhone { get; set; }            // Whatsapp Telefon Numarası
        public bool SmsApproval { get; set; }                 // SMS Onay

        // Teknik alanlar
        public bool FtpCreated { get; set; }                  // Site Ftp Oluştur (Hayır/Evet)
        public string Theme { get; set; } = "Yaprak Site";    // Site Teması

        // Pixel & Tracking
        public string? PixelCode { get; set; }                // Site Pixel kodu
        public string? PixelSpecialCode { get; set; }         // Site Özel Pixel kodu

        public string? PixelIds { get; set; }                 // Site Pixel ID'leri
        public string? CounterCode { get; set; }              // Site Counter Kodu

        // HTML içerikler
        public string? HtmlContent { get; set; }              // Site Orta Alan
        public string? FormHtml { get; set; }                 // Site Form Alanı
    }
}