namespace MonoCommerce.Entities;

public class DomainDetails
{
    public string Domain { get; set; } = null!;
    public string Status { get; set; } = "UNKNOWN"; // ACTIVE, EXPIRED, PENDING vb.
    public DateTime ExpirationDate { get; set; }
    public bool PrivacyEnabled { get; set; } = false;
    public string[] NameServers { get; set; } = Array.Empty<string>();
    // İhtiyaca göre DNS kayıtları, contact info vs. eklenebilir
}