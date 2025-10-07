using MonoCommerce.Data.Abstract;
using MonoCommerce.Entities.Domain;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonoCommerce.Entities.Domain;
using MonoCommerce.Entities;

namespace MonoCommerce.Data.Concrete
{
    public class GoDaddyRepository : IGoDaddyRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public GoDaddyRepository(HttpClient httpClient, string apiKey, string apiSecret)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        // 🔍 Domain detaylarını getir
        public async Task<DomainDetails> GetDomainDetailsAsync(string domain)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"https://api.ote-godaddy.com/v1/domains/{domain}");
            request.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DomainDetails>(json)!;
        }

        // 💡 Domain kullanılabilir mi kontrol et
        public async Task<bool> IsDomainAvailableAsync(string domain)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"https://api.ote-godaddy.com/v1/domains/available?domain={domain}"
                );

                request.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return false; // 422, 400 gibi durumlarda false döndür

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("available").GetBoolean();
            }
            catch
            {
                return false;
            }
        }

        // 🧾 Domain satın alma (test ortamı - OTE)
        public async Task<bool> PurchaseDomainAsync(DomainPurchaseRequest request)
        {
            var url = "https://api.ote-godaddy.com/v1/domains/purchase";

            var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");
            httpRequest.Content = httpContent;

            try
            {
                var response = await _httpClient.SendAsync(httpRequest);
                var body = await response.Content.ReadAsStringAsync();

                Console.WriteLine("========== GoDaddy PurchaseDomainAsync ==========");
                Console.WriteLine($"Request:\n{json}");
                Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}");
                Console.WriteLine($"Response:\n{body}");
                Console.WriteLine("=================================================");

                // 200 veya 201 başarılı kabul edilir
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Hata: {ex.Message}");
                return false;
            }
        }


        // DNS kayıtlarını getir
        public async Task<DnsRecord[]> GetDnsRecordsAsync(string domain)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"https://api.ote-godaddy.com/v1/domains/{domain}/records");
            request.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DnsRecord[]>(json)!;
        }

        // // DNS kayıtlarını güncelle
        // public async Task<bool> UpdateDnsRecordsAsync(string domain, DnsRecord[] records)
        // {
        //     var url = $"https://api.ote-godaddy.com/v1/domains/{domain}/records";
        //     var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
        //     {
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //         WriteIndented = true
        //     });

        //     var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //     var request = new HttpRequestMessage(HttpMethod.Put, url);
        //     request.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");
        //     request.Content = httpContent;

        //     var response = await _httpClient.SendAsync(request);
        //     return response.IsSuccessStatusCode;
        // }

        public async Task<bool> UpdateDnsAsync(DnsUpdateRequest request, bool simulate = true)
        {
            if (simulate)
            {
                Console.WriteLine("========== GoDaddy DNS Update (Simulate) ==========");
                Console.WriteLine($"Domain: {request.Domain}");
                foreach (var r in request.Records)
                {
                    Console.WriteLine($"Record => Name: {r.Name}, Type: {r.Type}, Data: {r.Data}, TTL: {r.Ttl}");
                }
                Console.WriteLine("===================================================");
                return true;
            }

            // Gerçek API çağrısı (sonradan açılacak)
            var url = $"https://api.ote-godaddy.com/v1/domains/{request.Domain}/records";
            var json = JsonSerializer.Serialize(request.Records, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, url);
            httpRequest.Headers.Add("Authorization", $"sso-key {_apiKey}:{_apiSecret}");
            httpRequest.Content = httpContent;

            var response = await _httpClient.SendAsync(httpRequest);
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Status: {(int)response.StatusCode} {response.ReasonPhrase}");
            Console.WriteLine($"Response Body: {body}");
            return response.IsSuccessStatusCode;
        }



    }
}