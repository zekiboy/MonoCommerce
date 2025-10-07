using MonoCommerce.Business.Abstract;
using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities;
using MonoCommerce.Entities.Domain;
using System.Threading.Tasks;

namespace MonoCommerce.Business
{
    public class GoDaddyManager : IGoDaddyManager
    {
        private readonly IGoDaddyRepository _goDaddyRepository;

        public GoDaddyManager(IGoDaddyRepository goDaddyRepository)
        {
            _goDaddyRepository = goDaddyRepository;
        }

        // Domain işlemleri
        public async Task<bool> IsDomainAvailableAsync(string domain)
            => await _goDaddyRepository.IsDomainAvailableAsync(domain);

        public async Task<DomainDetails> GetDomainDetailsAsync(string domain)
            => await _goDaddyRepository.GetDomainDetailsAsync(domain);

        public async Task<bool> PurchaseDomainAsync(DomainPurchaseRequest request)
            => await _goDaddyRepository.PurchaseDomainAsync(request);

        // DNS işlemleri
        public async Task<DnsRecord[]> GetDnsRecordsAsync(string domain)
            => await _goDaddyRepository.GetDnsRecordsAsync(domain);

        public async Task<bool> UpdateDnsAsync(DnsUpdateRequest request, bool simulate = true)
            => await _goDaddyRepository.UpdateDnsAsync(request, simulate);
    }
}