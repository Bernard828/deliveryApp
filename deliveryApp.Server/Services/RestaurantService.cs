using deliveryApp.Server.Data;
using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.NewFolder
{
    public interface IRestaurantService
    {
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<Restaurant> CreateAsync(RestaurantCreateDto dto);
        Task<bool> UpdateAsync(RestaurantUpdateDto dto);
        Task<bool> EditMultipleAsync(RestaurantUpdateMultipleDto dto);
        Task<bool> DeleteTagAsync(int id, List<string> tagNames);
        Task<IEnumerable<RestaurantSearchDto>> SearchAsync(string query);
        Task<bool> DeleteRestaurantAsync(int id);
        Task<bool> UpdateOperatingHoursAsync(int restaurantId, List<RestaurantHourDto> hours);
        Task<IEnumerable<RestaurantDto>> GetPagedAsync(int page, int pageSize);

    }

    public class RestaurantService : IRestaurantService
    {
        private readonly DeliveryAppDbContext _context;

        public RestaurantService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            var restaurants = await _context.Restaurants
                 //.Include(r => r.CuisineType)
                 //.Include(r => r.OperatingHours)
                 //.Include(r => r.SearchTags)
                 .ToListAsync();
            //return Ok(restaurants);
            return restaurants.Select(MapToDto);
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.CuisineType)
                .Include(r => r.OperatingHours)
                .Include(r => r.SearchTags)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            return restaurant == null ? null : MapToDto(restaurant);
        }
        public async Task<IEnumerable<RestaurantSearchDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<RestaurantSearchDto>();

            var lower = query.ToLower();

            return await _context.Restaurants
                .Include(r => r.CuisineType)
                .Include(r => r.MenuItems)
                .Include(r => r.SearchTags)
                .Where(r =>
                r.Name.ToLower().Contains(lower) ||
                r.Description.ToLower().Contains(lower) ||
                r.CuisineType != null && r.CuisineType.Name.ToLower().Contains(lower) ||
                r.SearchTags.Any(t => t.TagName.ToLower().Contains(lower)) ||
                r.MenuItems.Any(m =>
                m.Name.ToLower().Contains(lower) ||
               m.SearchTags.ToLower().Contains(lower)
               )
                )
                .Select(r => new RestaurantSearchDto
                {
                    RestaurantId = r.RestaurantId,
                    Name = r.Name,
                    Description = r.Description,
                    CuisineTypeId = r.CuisineTypeId ?? 0,
                    MenuItems = r.MenuItems
                    .Where(m =>
                    m.Name.ToLower().Contains(lower) ||
                    m.SearchTags.ToLower().Contains(lower))
                    .Select(m => new MenuItemSearchDto
                    {
                        MenuItemId = m.MenuItemId,
                        Name = m.Name,
                        Price = m.Price,
                        SearchTags = m.SearchTags,
                        RestaurantId = m.RestaurantId
                    })
                })
                .ToListAsync();
        }
        public async Task<Restaurant> CreateAsync(RestaurantCreateDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                CuisineTypeId = dto.CuisineTypeId,
                //Address = dto.Address,
                //Price = dto.Price,
                //ImageUrl = dto.ImageUrl,
                SearchTags = dto.SearchTags
                .Select(t => new RestaurantTag
                { TagName = t })
                .ToList(),
                //OperatingHours = dto.OperatingHours.Select(h => new RestaurantHour
                //{
                //    DayOfWeek = h.DayOfWeek,
                //    OpenTime = TimeSpan.Parse(h.OpenTime),
                //    CloseTime = TimeSpan.Parse(h.CloseTime)
                //}).ToList()
            };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }

        public async Task<bool> UpdateAsync(RestaurantUpdateDto dto)
        {
            var restaurant = await _context.Restaurants
                .FindAsync(dto.RestaurantId);

            if (restaurant == null) return false;
            //if(restaurant ==null || id != restaurant.RestaurantId)
            restaurant.Name = dto.Name;
            restaurant.CuisineTypeId = dto.CuisineTypeId;

            //restaurant.Address = restaurantDto.Address;
            //restaurant.Price = restaurantDto.Price;
            //restaurant.ImageUrl = restaurantDto.ImageUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditMultipleAsync(RestaurantUpdateMultipleDto dto)
        {
            var restaurants = await _context.Restaurants
                .Where(r => dto.RestaurantIds.Contains(r.RestaurantId))
                .Include(r => r.SearchTags)
                .ToListAsync();

            if (!restaurants.Any()) return false;
            foreach (var r in restaurants)
            {
                if (dto.Description != null)
                    r.Description = dto.Description;

                if (dto.CuisineTypeId.HasValue)
                    r.CuisineTypeId = dto.CuisineTypeId;
                if (dto.AddSearchTags != null)
                {
                    foreach (var tag in dto.AddSearchTags)
                    {
                        if (!r.SearchTags.Any(t => t.TagName == tag))
                        {
                            r.SearchTags.Add(new RestaurantTag { TagName = tag });
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTagAsync(int id, List<string> tagNames)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.SearchTags)
                .FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (restaurant == null) return false;

            // restaurant.SearchTags.Remove(t => tagNames.Contains(t.TagName));

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOperatingHoursAsync(int restaurantId, List<RestaurantHourDto> hours)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.OperatingHours)
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            if (restaurant == null) return false;

            _context.RestaurantHours.RemoveRange(restaurant.OperatingHours);

            restaurant.OperatingHours = hours.Select(h => new RestaurantHour
            {
                DayOfWeek = h.DayOfWeek,
                OpenTime = TimeSpan.Parse(h.OpenTime),
                CloseTime = TimeSpan.Parse(h.CloseTime)
            }).ToList();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RestaurantDto>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            var totalCount = await _context.Restaurants.CountAsync();

            var restaurants = await _context.Restaurants
                .Include(r => r.CuisineType)
                .Include(r => r.OperatingHours)
                .Include(r => r.SearchTags)
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return restaurants.Select(MapToDto);
            //return new PagedResult<RestaurantDto>
            //{
            //    Items = restaurants.Select(MapToDto).ToList(),
            //    totalCount = totalCount,
            //    page = page,
            //    pageSize = pageSize
            //};
        }

        private static RestaurantDto MapToDto(Restaurant r)
        {
            var now = DateTime.Now;
            var today = now.DayOfWeek;
            var time = now.TimeOfDay;

            var todaysHours = r.OperatingHours.FirstOrDefault(h => h.DayOfWeek == today);
            bool isOpen = todaysHours != null &&
                (todaysHours.OpenTime <= todaysHours.CloseTime
                ? time >= todaysHours.OpenTime && time <= todaysHours.CloseTime
                : time >= todaysHours.OpenTime || time <= todaysHours.CloseTime);

            return new RestaurantDto
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                CuisineTypeId = r.CuisineTypeId ?? 0,
                Cuisine = r.CuisineType!,
                Price = r.Price,
                ImageUrl = r.ImageUrl,
                Address = r.Address,
                IsCurrentlyOpen = isOpen,
                OperatingHours = r.OperatingHours
                .OrderBy(h => h.DayOfWeek)
                .Select(h => new RestaurantHourDto
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                    CloseTime = h.CloseTime.ToString(@"hh\:mm")
                }).ToList()
            };
        }
    }
}