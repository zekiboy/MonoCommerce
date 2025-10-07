using MonoCommerce.Entities.Domain;
using System.Threading.Tasks;

namespace MonoCommerce.Business.Abstract
{
    public interface IHtmlExportManager
    {
        Task<bool> ExportToLocalAsync(HtmlExportRequest request);
        Task<bool> ExportToServerAsync(HtmlExportRequest request);
    }
}