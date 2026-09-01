using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    public class RestaurantTag
    {
        [Key] public int RestaurantTagId { get; set; }
        public int RestaurantId { get; set; }
        [Required] public string TagName { get; set; } = string.Empty;
    }
}
