using System.ComponentModel.DataAnnotations;

namespace deliveryApp.Server.DTOs
{
    public sealed class RestaurantTagCreateDto
    {
        [Required,MaxLength(50)]
        public string? Name { get; set; }
    }
}
