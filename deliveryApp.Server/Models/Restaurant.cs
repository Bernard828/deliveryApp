using deliveryApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryApp.Server.Models
{
    public class Restaurant
    {
        [Key] public int RestaurantId { get; set; }
        [Required,MaxLength(200)] public string Name { get; set; } = string.Empty;
        [Required,MaxLength(500)] public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }       
        public int? CuisineTypeId { get; set; }
        public virtual CuisineType? CuisineType { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; } = null!;
        public virtual ICollection<RestaurantTag> SearchTags { get; set; } = new List<RestaurantTag>();
        public virtual ICollection<OperatingHours> OperatingHours { get; set; } = new List<OperatingHours>();
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
