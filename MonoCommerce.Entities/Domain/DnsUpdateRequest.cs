namespace MonoCommerce.Entities.Domain
{
    public class DnsUpdateRequest
    {
        // public string Domain { get; set; } = null!; // güncellenecek domain
        // public DnsRecord[] Records { get; set; } = Array.Empty<DnsRecord>();

        public string Domain { get; set; } = null!;
        public DnsRecord[] Records { get; set; } = Array.Empty<DnsRecord>();
    }
}