namespace deliveryApp.Server.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CuisineTypeId { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public virtual CuisineType Cuisine { get; set; } = null!;
    }
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CuisineTypeId { get; set; }
        public virtual CuisineType Cuisine { get; set; } = null!;
        public int Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class CuisineType
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        //Navigation property for the related Restaurants
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
