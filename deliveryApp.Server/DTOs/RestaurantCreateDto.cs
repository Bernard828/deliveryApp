using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantCreateDto
    {
        [Required, MaxLength(150)] public string Name { get; set; } = string.Empty;
        [MaxLength(1000)] public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        public int? CuisineTypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        //public int? AddressId { get; set; }
        public AddressDto? Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<OperatingHoursCreateDto> OperatingHours { get; set; } = new();
    }

}
