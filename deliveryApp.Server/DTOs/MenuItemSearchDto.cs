namespace deliveryApp.Server.DTOs
{
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
}
