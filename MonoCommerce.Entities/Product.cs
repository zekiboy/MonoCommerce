using MonoCommerce.Entities;

namespace MonoCommerce.Entities
{
    public class Product : BaseEntity
    {
        // Zorunlu alanlar
        public string Name { get; set; } = null!;              // Ürün Adı
        public string? ShortDescription { get; set; } = null!; // Ürün Kısa Açıklama
        public bool IsActive { get; set; }                    // Ürün Durumu (Aktif / Pasif)
        
        // kategori , marka, ürün tipi, vb. ilişkiler ekle 


        // Opsiyonel alanlar
        public string? Barcode { get; set; }                  // Ürün Barkod
        public string? StockCode { get; set; }                // Ürün Stok Kodu
        public string? Sku { get; set; }                      // Benzersiz stok kodu
        public int? StockQuantity { get; set; }                // Stok miktarı

        public decimal? Price { get; set; }                    // Ürün Fiyat
        public decimal? SalePrice { get; set; }                // Ürün Satış Fiyatı
        public decimal? DiscountRate { get; set; }            // Ürün İndirim Oranı
        public decimal? PurchasePrice { get; set; }           // Ürün Alış Fiyatı

        public decimal? CargoWeightKg { get; set; }           // Kargo Ağırlığı (KG)
        public decimal? Desi { get; set; }                    // Desi
        public string? PromotionSite { get; set; }            // Ürün Tanıtım Sitesi

        // Firma ilişkisi (FK mantığıyla)
        public CargoCompany? CargoCompany { get; set; } = null!;
        public int? CargoCompanyId { get; set; } // Ürünü gönderecek kargo firması

        // Extra alanlar
        public string? ImagePath { get; set; }                // Resim Yükle
        public bool? ShowInECommerceTheme { get; set; }        // E-Ticaret Temasında Göster
        public string? YoutubeLink { get; set; }              // Youtube Video Linki
        public string? HtmlContent { get; set; }              // Ürün Orta Alan (HTML kodu)

        // SEO alanları
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }

        // Etiketler
        public string? Tags { get; set; }

        // Kullanıcı izleme
        public int? CreatedByUserId { get; set; }
    }
}