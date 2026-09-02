using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    [Owned]
    public class Address
    {
        public int AddressId { get; set; }
        [Required, MaxLength(150)] public string Line1 { get; set; } = string.Empty;
        [MaxLength(150)] public string Line2 { get; set; } = string.Empty;
        [Required, MaxLength(100)] public string City { get; set; } = string.Empty;
        [Required, MaxLength(50)] public string State { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string PostalCode { get; set; } = string.Empty;
        [Required, MaxLength(100)] public string Country { get; set; } = string.Empty;
    }

}
