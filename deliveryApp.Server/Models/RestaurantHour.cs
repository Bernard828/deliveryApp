using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.Models
{
    public class RestaurantHour
    {
        [Key] public int RestaurantHourId { get; set; }
        public int RestaurantId { get; set; }
        [Required] public DayOfWeek DayOfWeek { get; set; }
        [Required] public TimeSpan OpenTime { get; set; }
        [Required] public TimeSpan CloseTime { get; set; }
    }
}
