using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryApp.Server.Models
{
    public class MenuItem
    {
        [Key] public int MenuItemId { get; set; }
        [Required, MaxLength(150)] public string Name { get; set; } = string.Empty;
        [MaxLength(500)] public string Description { get; set; } = string.Empty;
        [Required, Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
        [MaxLength(500)] public string ImageUrl { get; set; } = string.Empty;
        [Required] public int RestaurantId { get; set; }
        [ForeignKey(nameof(RestaurantId))] public  Restaurant? Restaurant { get; set; }
        public virtual ICollection<MenuItemTag> SearchTags { get; set; } = new List<MenuItemTag>();
    }
}
