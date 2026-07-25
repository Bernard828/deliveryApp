namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        //public int? CuisineTypeId { get; set; }
    }
}
