using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class OperatingHourUpdateDto
    {
        //[Required] public int RestaurantId { get; set; }
        [Range(0, 6)] public int DayOfWeek { get; set; }
        public bool IsClosed { get; set; }
        public TimeOnly? OpenTime { get; set; }
        public TimeOnly? CloseTime { get; set; }
        //public DayOfWeek DayOfWeek { get; set; }
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        //public string OpenTime { get; set; } = string.Empty;
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        //public string CloseTime { get; set; } = string.Empty;

    }
}
