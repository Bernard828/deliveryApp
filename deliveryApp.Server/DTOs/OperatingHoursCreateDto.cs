using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class OperatingHoursCreateDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        public string OpenTime { get; set; } = string.Empty;
        [RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        public string CloseTime { get; set; } = string.Empty;
    }
}
