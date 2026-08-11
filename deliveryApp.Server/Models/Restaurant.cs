using deliveryApp.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    public class Restaurant
    {
        [Key] public int RestaurantId { get; set; }
        [Required][MaxLength(200)] public string Name { get; set; } = string.Empty;
        [Required][MaxLength(200)] public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
       
        public int? CuisineTypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public Address? Address { get; set; }
        public virtual CuisineType? CuisineType { get; set; } = null!;
        //public virtual ICollection<RestaurantTag> SearchTags { get; set; } = new List<RestaurantTag>();
        //public virtual ICollection<RestaurantHour> OperatingHours { get; set; } = new List<RestaurantHour>();
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
