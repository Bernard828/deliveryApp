using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryApp.Server.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))] public virtual Restaurant? Restaurant { get; set; }


        // FIX: Replaced List<string> with strongly typed child entities
        public virtual ICollection<MenuItemTag> SearchTags { get; set; } = new List<MenuItemTag>();
    }

    public class MenuItemTag
    {
        [Key]
        public int MenuItemTagId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int MenuItemId { get; set; }
    }

    public class MenuItemDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public List<string> SearchTags { get; set; } = new();
    }

    public class MenuItemSearchDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public List<string> SearchTags { get; set; } = new();
    }

    public class MenuItemCreateDto
    {
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
