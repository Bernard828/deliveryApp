using deliveryApp.Server.Models;

namespace deliveryApp.Server.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public string SearchTags { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int? CuisineTypeId { get; set; }

        public virtual CuisineType? CuisineType { get; set; } = null!;
        public virtual ICollection<RestaurantTag> SearchTags { get; set; } = new List<RestaurantTag>();
        public virtual ICollection<RestaurantHour> OperatingHours { get; set; } = new List<RestaurantHour>();
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

    public class RestaurantTag
    {
        public int RestaurantTagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
    public class RestaurantHour
    {
        public int RestaurantHourId { get; set; }
        public int RestaurantId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
    public class RestaurantCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> SearchTags { get; set; } = new();
        public int? CuisineTypeId { get; set; }
    }
    public class RestaurantUpdateDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CuisineTypeId { get; set; }
    }
    public class RestaurantUpdateMultipleDto
    {
        public List<int> RestaurantIds { get; set; } = new();
        //public List<int> RestaurantIds { get; set; } = new List<int>();
        public string? Description { get; set; }
        public int? CuisineTypeId { get; set; }
        public List<string>? AddSearchTags { get; set; }

    }

    public class DeleteRestaurantSearchTagsDto
    {
        public List<string> TagNames { get; set; } = new();
    }
    public class RestaurantSearchDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CuisineTypeId { get; set; }
        public IEnumerable<MenuItemSearchDto> MenuItems { get; set; } = Enumerable.Empty<MenuItemSearchDto>();

    }

    //public class RestaurantCuisine
    //{
    //    public int RestaurantId { get; set; }
    //    public Restaurant? Restaurant { get; set; }
    //    public int CuisineTypeId { get; set; }
    //    public CuisineType? CuisineType { get; set; }
    //}

    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CuisineTypeId { get; set; }
        public string CuisineName { get; set; } = string.Empty;
        public virtual CuisineType Cuisine { get; set; } = null!;

        public decimal Price { get; set; }
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
