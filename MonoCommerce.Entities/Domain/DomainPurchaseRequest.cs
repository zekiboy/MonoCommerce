namespace MonoCommerce.Entities.Domain
{
    public class DomainPurchaseRequest
    {
        public string Domain { get; set; } = null!;
        public int Period { get; set; } = 1;
        public bool RenewAuto { get; set; } = true;
        public bool Privacy { get; set; } = false;
        public string[] NameServers { get; set; } = Array.Empty<string>();
        
        public ContactInfo ContactAdmin { get; set; } = new ContactInfo();
        public ContactInfo ContactBilling { get; set; } = new ContactInfo();
        public ContactInfo ContactRegistrant { get; set; } = new ContactInfo();
        public ContactInfo ContactTech { get; set; } = new ContactInfo();
        
        public ConsentInfo Consent { get; set; } = new ConsentInfo();
    }

    public class ContactInfo
    {
        public string NameFirst { get; set; } = "Test";
        public string NameLast { get; set; } = "User";
        public string NameMiddle { get; set; } = "";
        public string Email { get; set; } = "test@example.com";
        public string Phone { get; set; } = "+1.4805058800";
        public string Fax { get; set; } = "";
        public string JobTitle { get; set; } = "";
        public string Organization { get; set; } = "";
        public Address AddressMailing { get; set; } = new Address();
    }

    public class Address
    {
        public string Address1 { get; set; } = "123 Main St";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "Scottsdale";
        public string State { get; set; } = "AZ";
        public string PostalCode { get; set; } = "85260";
        public string Country { get; set; } = "US";
    }

    public class ConsentInfo
    {
        public string[] AgreementKeys { get; set; } = new[] { "DNRA" };
        public string AgreedBy { get; set; } = "test@example.com";
        public string AgreedAt { get; set; } = DateTime.UtcNow.ToString("o");
    }
}