using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? CuisineTypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<OperatingHoursDto> OperatingHours { get; set; } = new();
    }
}
