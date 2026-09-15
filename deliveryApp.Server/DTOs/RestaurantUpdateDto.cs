using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public class RestaurantUpdateDto
    {
        [Required, MaxLength(150)] 
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? CuisineTypeId { get; set; }
        
        public string ImageUrl { get; set; } = string.Empty;


        public AddressUpdateDto? Address { get; set; } = new();
        public List<int> RestaurantTagIds { get; set; } = new();
        //public List<OperatingHourCreateDto> OperatingHours { get; set; } = new();
    }
}
