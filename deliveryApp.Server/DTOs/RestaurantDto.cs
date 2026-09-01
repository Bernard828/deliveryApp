using deliveryApp.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public bool IsCurrentlyOpen { get; set; }
        public int? AddressId { get; set; }
        public AddressDto? Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<RestaurantHourDto> OperatingHours { get; set; } = new();

        public int? CuisineTypeId { get; set; }
        public string? CuisineTypeName { get; set; } = string.Empty;
    }

    public class RestaurantCreateDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int? CuisineTypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int? AddressId { get; set; }
        public AddressDto? Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<RestaurantHourDto> OperatingHours { get; set; } = new();
    }

    public class RestaurantUpdateDto
    {
        [Required] public int RestaurantId { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? CuisineTypeId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new();
        public List<string> SearchTags { get; set; } = new();
        public List<RestaurantHourDto> OperatingHours { get; set; } = new();
    }

}