namespace deliveryApp.Server.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
       // public string SearchTags { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;    
        public int RestaurantId { get; set; }

        public virtual Restaurant? Restaurant { get; set; } = null!;
    }

    public class MenuItemTag
    {
        public int MenuItemTagId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MenuItemId { get; set; }
    }
    public class MenuItemSearchDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
       // public string SearchTags { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
    }

    public class MenuItemCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        //public string SearchTags { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
    }
    public class MenuItemUpdateDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
       // public string SearchTags { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
    }
}
