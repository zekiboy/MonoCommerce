using MonoCommerce.Entities;
using MonoCommerce.Entities.Domain;
using System.Threading.Tasks;

namespace MonoCommerce.Business.Abstract
{
    public interface IGoDaddyManager
    {
        // Domain işlemleri
        Task<bool> IsDomainAvailableAsync(string domain);
        Task<DomainDetails> GetDomainDetailsAsync(string domain);
        Task<bool> PurchaseDomainAsync(DomainPurchaseRequest request);

        // DNS işlemleri
        Task<DnsRecord[]> GetDnsRecordsAsync(string domain);
        Task<bool> UpdateDnsAsync(DnsUpdateRequest request, bool simulate = true);
        
    }
}