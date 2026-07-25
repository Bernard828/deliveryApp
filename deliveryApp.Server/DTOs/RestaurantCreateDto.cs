using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

       // public AddressDto? Address { get; set; }
        //public List<string>? SearchTags { get; set; } = new();
        //public int? CuisineTypeId { get; set; }
    }
}
