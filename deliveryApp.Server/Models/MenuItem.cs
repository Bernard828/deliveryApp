namespace deliveryApp.Server.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;    
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
