namespace deliveryApp.Server.DTOs
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // public AddressDto? Address { get; set; }
        //public int? CuisineTypeId { get; set; }
        //public List<string>? SearchTags { get; set; }
        //public string CuisineName { get; set; } = string.Empty;
        // public virtual CuisineType Cuisine { get; set; } = null!;
        //public decimal Price { get; set; }
        //public string ImageUrl { get; set; } = string.Empty;
        //public string Address { get; set; } = string.Empty;
        // public bool IsCurrentlyOpen { get; set; }
        //public List<RestaurantHourDto> OperatingHours { get; set; } = new();
    }
}
