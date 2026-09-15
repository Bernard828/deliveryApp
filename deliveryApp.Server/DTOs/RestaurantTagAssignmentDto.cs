namespace deliveryApp.Server.DTOs
{
    public class RestaurantTagAssignmentDto
    {
        public int Restaurant { get; set; }
        public List<int> RestaurantTagIds { get; set; } = new();
    }
}
