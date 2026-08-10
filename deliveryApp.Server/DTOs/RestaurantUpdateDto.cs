using deliveryApp.Server.Models;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CuisineTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public AddressDto? Address { get; set; }

    }
}
