using System.ComponentModel.DataAnnotations;

namespace MonoCommerce.WebUI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }  // BaseEntity’den gelir

        [Required(ErrorMessage = "Lütfen geçerli bir ürün adı girin.")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = null!;

        [Display(Name = "Kısa Açıklama")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int? StockQuantity { get; set; }

        [Display(Name = "Fiyat")]
        public decimal? Price { get; set; }

        [Display(Name = "Satış Fiyatı")]
        public decimal? SalePrice { get; set; }

        [Display(Name = "İndirim Oranı (%)")]
        public decimal? DiscountRate { get; set; }

        [Display(Name = "Kargo Firması")]
        public int? CargoCompanyId { get; set; }

        [Display(Name = "Resim")]
        public string? ImagePath { get; set; }

        [Display(Name = "Youtube Linki")]
        public string? YoutubeLink { get; set; }

        [Display(Name = "HTML İçerik")]
        public string? HtmlContent { get; set; }
    }
}