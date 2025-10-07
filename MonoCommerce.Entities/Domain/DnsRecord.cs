namespace MonoCommerce.Entities.Domain
{
    public class DnsRecord
    {
        // public string Type { get; set; } = "A"; // A, CNAME, MX, TXT vb.
        // public string Name { get; set; } = "@"; // root için @
        // public string Data { get; set; } = "127.0.0.1"; // IP veya CNAME hedefi
        // public int Ttl { get; set; } = 600; // saniye cinsinden TTL
        // public int? Priority { get; set; } // MX veya SRV kayıtları için

        //TEST İÇİN EKLEDİM
        public string Name { get; set; } = "www";
        public string Type { get; set; } = "A";
        public string Data { get; set; } = "127.0.0.1"; // test için localhost
        public int Ttl { get; set; } = 3600;

    }
}