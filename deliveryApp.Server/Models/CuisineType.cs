using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    public class CuisineType
    {
        [Key] public int CuisineTypeId { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    }
    public class CuisineTypeDto
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CuisineTypeCreateDto
    {
        public string Name { get; set; } = string.Empty;

    }
    public class CuisineTypeUpdateDto
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
