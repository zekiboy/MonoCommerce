namespace MonoCommerce.Entities.Domain
{
    public class HtmlExportRequest
    {
        public string Domain { get; set; } = null!;      // Export edilecek domain
        public string HtmlContent { get; set; } = null!; // Export edilecek HTML içeriği
        public string TargetPath { get; set; } = null!;  // Lokal veya hosting yolu
    }
}