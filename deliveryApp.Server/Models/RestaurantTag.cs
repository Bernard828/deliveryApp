namespace deliveryApp.Server.Models
{
    public class RestaurantTag
    {
        public int RestaurantTagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
}
