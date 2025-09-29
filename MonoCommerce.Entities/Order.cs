using System;

namespace MonoCommerce.Entities
{
    public class Order : BaseEntity
    {
        // Müşteri Bilgileri
        public string CustomerName { get; set; }          // İsim
        public string? CustomerPhone { get; set; }         // Telefon
        public string? CustomerEmail { get; set; }         // Opsiyonel
        public string? City { get; set; }                  // Şehir
        public string? Address { get; set; }               // Adres (detay için)

        // Sipariş Bilgileri
        // public int ProductId { get; set; }                // Hangi ürün sipariş edildi
        // public Product Product { get; set; }

        public int? SiteId { get; set; }                  // Sipariş hangi site üzerinden geldi
        public Site? Site { get; set; }

        public int? Quantity { get; set; }                 // Adet
        public decimal? UnitPrice { get; set; }            // Birim fiyat
        public decimal? TotalPrice { get; set; }           // Toplam fiyat

        // Sipariş Durumu
        public OrderStatus? Status { get; set; }           // Beklemede, Teyit Edildi, Tedarik, Kargoda, Teslim Edildi
        public bool? IsConfirmedByPhone { get; set; }      // Telefonla teyit edildi mi?

        // Tarihler
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }

    public enum OrderStatus
    {
        Eksik = 0,          // Bilgileri eksik sipariş
        Onayli = 1,         // Telefonla teyit alınmış sipariş
        Iptal = 2,          // İptal edilen sipariş
        Tedarik = 3,        // Ürün tedarik aşamasında
        Kargo = 4,          // Kargoya verildi
        Paket = 5,          // Paketleme aşamasında
        Teslim = 6,         // Müşteriye teslim edildi
        IleriTarihli = 7,   // Belirli ileri tarih için alınmış sipariş
        Cop = 8,            // Geçersiz / spam sipariş
        Ulasilamayan = 9,   // Telefonla ulaşılamayan müşteri
        Iade = 10           // Teslim sonrası iade edilmiş sipariş
    }
}