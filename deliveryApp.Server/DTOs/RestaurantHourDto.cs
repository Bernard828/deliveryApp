namespace deliveryApp.Server.DTOs
{
    public class RestaurantHourDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string OpenTime { get; set; } = string.Empty;
        public string CloseTime { get; set; } = string.Empty;
    }
}
