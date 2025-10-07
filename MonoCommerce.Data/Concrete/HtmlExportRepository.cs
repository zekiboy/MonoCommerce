using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities.Domain;
using System.IO;
using System.Threading.Tasks;

namespace MonoCommerce.Data.Concrete
{
    public class HtmlExportRepository : IHtmlExportRepository
    {
        public async Task<bool> ExportToLocalAsync(HtmlExportRequest request)
        {
            try
            {
                var dir = Path.GetDirectoryName(request.TargetPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(request.TargetPath, request.HtmlContent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ExportToServerAsync(HtmlExportRequest request)
        {
            // Şimdilik simülasyon
            Console.WriteLine($"HTML export to server simulated for domain: {request.Domain}");
            Console.WriteLine(request.HtmlContent[..Math.Min(200, request.HtmlContent.Length)] + "...");
            await Task.Delay(100); // Simulated delay
            return true;
        }
    }
}