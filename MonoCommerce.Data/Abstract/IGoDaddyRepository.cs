using MonoCommerce.Entities;
using MonoCommerce.Entities.Domain;

namespace MonoCommerce.Data.Abstract
{
    public interface IGoDaddyRepository 
    {
       // Domain işlemleri
        Task<DomainDetails> GetDomainDetailsAsync(string domain);
        Task<bool> IsDomainAvailableAsync(string domain);
        Task<bool> PurchaseDomainAsync(DomainPurchaseRequest request);

        // DNS işlemleri
        Task<DnsRecord[]> GetDnsRecordsAsync(string domain);
        Task<bool> UpdateDnsAsync(DnsUpdateRequest request, bool simulate = true);
    }
}