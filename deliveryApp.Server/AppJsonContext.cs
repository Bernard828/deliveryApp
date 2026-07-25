using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using System.Text.Json.Serialization;

namespace deliveryApp.Server
{
    // Explicitly register every class/DTO sent over your API endpoints
    [JsonSerializable(typeof(List<RestaurantDto>))]
    [JsonSerializable(typeof(RestaurantDto))]
    [JsonSerializable(typeof(RestaurantHourDto))]
    [JsonSerializable(typeof(CuisineType))]
    [JsonSerializable(typeof(Restaurant))]
    [JsonSerializable(typeof(RestaurantHour))]
    public partial class AppJsonContext : JsonSerializerContext
    {
    }
}
