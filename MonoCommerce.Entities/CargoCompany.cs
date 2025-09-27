namespace MonoCommerce.Entities
{
    public class CargoCompany : BaseEntity
    {

        // Temel bilgiler
        public string Name { get; set; } = null!;          // Kargo Firması
        public string? Description { get; set; }           // Açıklama

        // Ağırlık türü (Desi / KG / Desi-KG)
        public string WeightType { get; set; } = "Desi";   // Enum’a çevrilebilir

        // API Ayarları
        public string? ApiType { get; set; }               // Kargo Api Türü (örn: POSTMAN, SEÇKİN SOFT)
        public string? ApiUrl { get; set; }                // API URL
        public string? CarrierCargoUrlId { get; set; }     // TAŞIYICI KARGO URL ID (POSTMAN için)
        public string? ApiUsername { get; set; }           // API Kullanıcı Adı
        public string? ApiPassword { get; set; }           // API Şifre
        public string? ApiToken { get; set; }              // API Token

        // Durum
        public bool IsActive { get; set; } = true;         // Firma aktif/pasif durumu
    }
}