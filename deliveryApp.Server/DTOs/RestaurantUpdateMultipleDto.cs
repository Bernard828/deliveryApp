namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateMultipleDto
    {
        public List<int> RestaurantIds { get; set; } = new();
        //public List<int> RestaurantIds { get; set; } = new List<int>();
        public string? Description { get; set; }
        public int? CuisineTypeId { get; set; }
        public List<string>? AddSearchTags { get; set; }

    }
}
