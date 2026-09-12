using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryApp.Server.Models
{
    public class MenuItemTag
    {
        [Key]
        public int MenuItemTagId { get; set; }

        [Required,MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int MenuItemId { get; set; }

        [ForeignKey(nameof(MenuItemId))]
        public MenuItem? MenuItem { get; set; }
    }
}
