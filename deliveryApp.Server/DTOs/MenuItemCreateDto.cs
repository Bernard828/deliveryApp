using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class MenuItemCreateDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required, Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required] public int RestaurantId { get; set; }
        public List<int> MenuItemTagIds { get; set; } = new();
    }
}
