using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateDto
    {
        [Required,MaxLength(150)]public int RestaurantId { get; set; }
        [MaxLength(1000)]public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
       
        public int? CuisineTypeId { get; set; }
        
        public AddressDto? Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<OperatingHoursCreateDto> OperatingHours { get; set; } = new();
    }
}
