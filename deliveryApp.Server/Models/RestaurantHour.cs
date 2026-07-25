namespace deliveryApp.Server.Models
{
    public class RestaurantHour
    {
        public int RestaurantHourId { get; set; }
        public int RestaurantId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
}
