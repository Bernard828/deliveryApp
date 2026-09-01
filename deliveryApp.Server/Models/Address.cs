using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Models
{
    [Owned]
    public class Address
    {
        public int AddressId { get; set; }
        public string Line1 { get; set; } = string.Empty;
        public string Line2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
   
}
