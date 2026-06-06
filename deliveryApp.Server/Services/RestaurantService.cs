using deliveryApp.Server.Data;
using deliveryApp.Server.Models;

namespace deliveryApp.Server.NewFolder
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantyIdAsync(int id);
        Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto);
        Task<bool> UpdateRestaurantAsync(int id, RestaurantDto restaurantDto);
        Task<bool> DeleteRestaurantAsync(int id);
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
                Address = restaurantDto.Address,
                Price = restaurantDto.Price,
                ImageUrl = restaurantDto.ImageUrl,
                OperatingHours = restaurantDto.OperatingHours.Select(h => new RestaurantHour
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = TimeSpan.Parse(h.OpenTime),
                    CloseTime = TimeSpan.Parse(h.CloseTime)
                }).ToList()
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurantDto.RestaurantId = restaurant.RestaurantId;
            return restaurantDto;
        }

        public async Task<bool> UpdateRestaurantAsync(int id, RestaurantDto restaurantDto)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.OperatingHours)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null) return false;
            restaurant.Name = restaurantDto.Name;
            restaurant.cuisineTypeId = restaurantDto.CuisineTypeId;
            restaurant.Address = restaurantDto.Address;
            restaurant.Price = restaurantDto.Price;
            restaurant.ImageUrl = restaurantDto.ImageUrl;

            _context.RestaurantHours.RemoveRange(restaurant.OperatingHours);

            restaurant.OperatingHours = restaurantDto.OperatingHours
                .Select(
                h => new RestaurantHour
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = TimeSpan.Parse(h.OpenTime),
                    CloseTime = TimeSpan.Parse(h.CloseTime)
                }).ToList();

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateCurrencyException)
            {
                if (!RestaurantExists(id)) return false;
                throw;
            }
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesasync();
            return true;
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }

        private static RestaurantDto MapToDto(Restaurant restaurant)
        {
            var now = DateTime.Now;
            var currentDay = now.DayOfWeek;
            var currentTime = now.TimeOfDay;
            var todaysHours = restaurant.OperatingHours.FirstOrDefault
                (h => h.DayOfWeek == currentDay);

            bool isOpen = false;
            if (todaysHours != null)
            {
                if (todaysHours.OpenTime <= todaysHours.CloseTime)
                {
                    isOpen = currentTime >= todaysHours.OpenTime && currentTime <= todaysHours.CloseTime;
                }
                else
                {
                    isOpen = currentTime >= todaysHours.OpenTime || currentTime <= todaysHours.CloseTime;
                }
            }

            return new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                CuisineTypeId = restaurant.CuisineTypeId,
                Cuisine = restaurant.Cuisine,
                Price = restaurant.Price,
                ImageUrl = restaurant.ImageUrl,
                Address = restaurant.Address,
                IsCurrentlyOpen = isOpen,
                OperatingHours = restaurant.OperatingHours.Select(h => new RestaurantHourDto
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                    CloseTime = h.CloseTime.ToString(@"hh\:mm")
                }).OrderBy(h => h.DayOfWeek).ToList()
            };
        }
    }
}