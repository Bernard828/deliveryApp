using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    public class CuisineType
    {
        [Key] public int CuisineTypeId { get; set; }
        [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    }
}
