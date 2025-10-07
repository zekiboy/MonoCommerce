using MonoCommerce.Entities.Domain;
using System.IO;
using System.Threading.Tasks;

namespace MonoCommerce.Data.Abstract
{
    public interface IHtmlExportRepository
    {
        Task<bool> ExportToLocalAsync(HtmlExportRequest request);
        Task<bool> ExportToServerAsync(HtmlExportRequest request); // İleride gerçek hosting için
    }
}