using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonoCommerce.Entities;

namespace MonoCommerce.WebUI.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        // Müşteri Bilgileri
        [Required(ErrorMessage = "Müşteri adı gerekli.")]
        [Display(Name = "Müşteri Adı")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon girin.")]
        public string? CustomerPhone { get; set; }

        [Display(Name = "E-posta")]
        public string? CustomerEmail { get; set; }

        [Display(Name = "Şehir")]
        public string? City { get; set; }

        [Display(Name = "Adres")]
        public string? Address { get; set; }

        // // Sipariş Bilgileri
        // [Required(ErrorMessage = "Ürün seçimi gerekli.")]
        // [Display(Name = "Ürün")]
        // public int ProductId { get; set; }
        [Required(ErrorMessage = "Lütfen bir site seçin.")]
        [Display(Name = "Site")]
        public int? SiteId { get; set; } // Opsiyonel

        [Range(1, int.MaxValue, ErrorMessage = "Adet en az 1 olmalı.")]
        [Display(Name = "Adet")]
        public int? Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Geçerli bir fiyat girin.")]
        [Display(Name = "Birim Fiyat")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Toplam Fiyat")]
        public decimal? TotalPrice { get; set; } // Controller'da hesaplanacak

        // Sipariş Durumu
        [Required]
        [Display(Name = "Sipariş Durumu")]
        public OrderStatus? Status { get; set; }

        [Display(Name = "Telefonla Onaylandı")]
        public bool IsConfirmedByPhone { get; set; }

        // Tarihler (opsiyonel)
        [Display(Name = "Sipariş Tarihi")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        // Görüntüleme için
        // public string ProductName { get; set; } // Listeleme/detay sayfası
        public string? SiteName { get; set; }    // Listeleme/detay sayfası

        // Dropdown listeler
        // public SelectList Products { get; set; } // ViewBag yerine istersen buradan da kullanabilirsin
        // public SelectList Sites { get; set; }
    }
}