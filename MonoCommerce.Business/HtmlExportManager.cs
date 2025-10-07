using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities.Domain;
using System.Threading.Tasks;

namespace MonoCommerce.Business
{
    public class HtmlExportManager : IHtmlExportManager
    {
        private readonly IHtmlExportRepository _repo;

        public HtmlExportManager(IHtmlExportRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExportToLocalAsync(HtmlExportRequest request)
        {
            return await _repo.ExportToLocalAsync(request);
        }

        public async Task<bool> ExportToServerAsync(HtmlExportRequest request)
        {
            return await _repo.ExportToServerAsync(request);
        }
    }
}