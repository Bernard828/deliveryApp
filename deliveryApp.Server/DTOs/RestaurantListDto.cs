namespace deliveryApp.Server.DTOs
{
    public class RestaurantListDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CuisineTypeName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public AddressDto? Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrentlyOpen { get; set; }
    }
}
