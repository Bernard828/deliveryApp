using deliveryApp.Server.Data;
using deliveryApp.Server.Models;

namespace deliveryApp.Server.NewFolder
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantyIdAsync(int id);
        Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly DeliveryAppDb _context;
        public RestaurantService(DeliveryAppDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Cuisine)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Cuisine)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            return restaurant == null ? null : MapToDto(restaurant);
        }

        public async Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                CuisineTypeId = restaurantDto.CuisineTypeId,
                Address = restaurantDto.Address,,
                Price = restaurantDto.Price,
                ImageUrl = restaurantDto.ImageUrl
            };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurantDto.RestaurantId = restaurant.RestaurantId;
            return restaurantDto;
        }

        private static RestaurantDto MapToDto(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                CuisineTypeId = restaurant.CuisineTypeId,
                Cuisine = restaurant.Cuisine,
                Price = restaurant.Price,
                ImageUrl = restaurant.ImageUrl,
                Address = restaurant.Address
            };
        }
    }
}
}
