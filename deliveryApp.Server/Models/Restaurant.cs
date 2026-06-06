using deliveryApp.Server.Models;

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
        public virtual ICollection<RestaurantHour> OperatingHours { get; set; } = new List<RestaurantHour>();
    }

    public class CuisineType
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        //Navigation property for the related Restaurants
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }

    public class RestaurantHour
    {
        public int RestaurantHourId { get; set; }
        public int RestaurantId { get; set; }
        public DayOfWeeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
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
        public bool IsCurrentlyOpen { get; set; }
        public List<RestaurantHourDto> OperatingHours { get; set; } = new();
    }

    public class RestaurantHourDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string OpenTime { get; set; } = string.Empty;
        public string CloseTime { get; set; } = string.Empty;
    }
}
