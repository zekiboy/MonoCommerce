namespace MonoCommerce.WebUI.Models
{
    public class CargoCompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string WeightType { get; set; } = "Desi";
        public string? ApiType { get; set; }
        public string? ApiUrl { get; set; }
        public string? CarrierCargoUrlId { get; set; }
        public string? ApiUsername { get; set; }
        public string? ApiPassword { get; set; }
        public string? ApiToken { get; set; }
        public bool IsActive { get; set; }
    }
}