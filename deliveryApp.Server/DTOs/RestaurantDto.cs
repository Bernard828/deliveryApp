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
        public bool IsCurrentlyOpen { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;

        public int? CuisineTypeId { get; set; }
        public string? CuisineTypeName { get; set; } = string.Empty;

        public int? AddressId { get; set; }
        public AddressDto? Address { get; set; } = new();
        
        public List<string> SearchTags { get; set; } = new();
        public List<OperatingHoursDto> OperatingHours { get; set; } = new();
        public List<MenuItemDto> MenuItems { get; set; } = new();

    }
}