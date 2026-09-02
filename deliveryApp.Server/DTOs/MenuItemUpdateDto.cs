using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class MenuItemUpdateDto
    {
        [Required]
        public int MenuItemId { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required, Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int RestaurantId { get; set; }
        public List<string> SearchTags { get; set; } = new();
    }
}
