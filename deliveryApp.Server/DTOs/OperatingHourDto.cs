using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class OperatingHourDto
    {
        public int OperatingHourId { get; set; }
        // 0 = Sunday, 1 = Monday, 2 = Tuesday, 3 = Wednesday, 4 = Thursday, 5 = Friday, 6 = Saturday
        public int DayOfWeek { get; set; }
        public string DayName { get; set; } = string.Empty;
        public bool IsClosed { get; set; }
        public TimeOnly? OpenTime { get; set; }
        public TimeOnly? CloseTime { get; set; }
        //public DayOfWeek DayOfWeek { get; set; }
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        //public string OpenTime { get; set; } = string.Empty;
        //[RegularExpression(@"^([01]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Format must be HH:mm")]
        //public string CloseTime { get; set; } = string.Empty;

        public int RestaurantId { get; set; }
    }
}
