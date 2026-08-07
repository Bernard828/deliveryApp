using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace deliveryApp.Server.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto);
        Task<bool> UpdateAsync(RestaurantUpdateDto dto);
        Task<RestaurantDto?> GetByIdAsync(int id);
        //Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<List<RestaurantDto>> GetAllAsync();
        //Task<bool> EditMultipleAsync(RestaurantUpdateMultipleDto dto);
        //Task<bool> DeleteTagAsync(int id, List<string> tagNames);
        //Task<IEnumerable<RestaurantSearchDto>> SearchAsync(string query);
        Task<bool> DeleteRestaurantAsync(int id);
        //Task<bool> UpdateOperatingHoursAsync(int restaurantId, List<RestaurantHourDto> hours);
        //Task<IEnumerable<RestaurantDto>> GetPagedAsync(int page, int pageSize);

    }
    public class RestaurantService : IRestaurantService
    {
        private readonly DeliveryAppDbContext _context;

        public RestaurantService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                //CuisineTypeId = dto.CuisineTypeId,
                //SearchTagsJson = dto.SearchTags != null ? JsonSerializer.Serialize(dto.SearchTags) : null,
                //Address = dto.Address != null ? new Address
                //{
                //    Line1 = dto.Address.Line1,
                //    Line2 = dto.Address.Line2,
                //    City = dto.Address.City,
                //    State = dto.Address.State,
                //    PostalCode = dto.Address.PostalCode,
                //    Country = dto.Address.Country
                //} : null
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return MapToDto(restaurant);
        }

        public async Task<bool> UpdateAsync(RestaurantUpdateDto dto)
        {
            var restaurant = await _context.Restaurants
                .FindAsync(dto.RestaurantId);
            if (restaurant == null) return false;

            restaurant.Name = dto.Name;
            //restaurant.CuisineTypeId = dto.CuisineTypeId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RestaurantDto?> GetByIdAsync(int id)
        {
            var r = await _context.Restaurants.FindAsync(id);

            return r == null ? null : MapToDto(r);
        }

        public async Task<List<RestaurantDto>> GetAllAsync()
        {
            var list = await _context.Restaurants.ToListAsync();

            return list.Select(MapToDto).ToList();
        }

        //public async Task<IEnumerable<RestaurantSearchDto>> SearchAsync(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //        return Enumerable.Empty<RestaurantSearchDto>();

        //    var lower = query.ToLower();

        //    return await _context.Restaurants
        //        //.Include(r => r.CuisineType)
        //        .Include(r => r.MenuItems)
        //        //.Include(r => r.SearchTags)
        //        .Where(r =>
        //        r.Name.ToLower().Contains(lower) ||
        //        r.Description.ToLower().Contains(lower) //||
        //       // r.CuisineType != null && r.CuisineType.Name.ToLower().Contains(lower) ||
        //        //r.SearchTags.Any(t => t.TagName.ToLower().Contains(lower)) ||
        //       // r.MenuItems.Any(m =>
        //       // m.Name.ToLower().Contains(lower) ||
        //       //m.SearchTags.ToLower().Contains(lower)
        //       //)
        //       // )
        //        .Select(r => new RestaurantSearchDto
        //        {
        //            RestaurantId = r.RestaurantId,
        //            Name = r.Name,
        //            Description = r.Description,
        //            //CuisineTypeId = r.CuisineTypeId ?? 0,
        //            MenuItems = r.MenuItems
        //            //.Where(m =>
        //            //m.Name.ToLower().Contains(lower) ||
        //            //m.SearchTags.ToLower().Contains(lower))
        //            .Select(m => new MenuItemSearchDto
        //            {
        //                MenuItemId = m.MenuItemId,
        //                Name = m.Name,
        //               // Price = m.Price,
        //                //SearchTags = m.SearchTags,
        //                RestaurantId = m.RestaurantId
        //            })
        //        })
        //        .ToListAsync();
        //}

        //public async Task<bool> EditMultipleAsync(RestaurantUpdateMultipleDto dto)
        //{
        //    var restaurants = await _context.Restaurants
        //        .Where(r => dto.RestaurantIds.Contains(r.RestaurantId))
        //        //.Include(r => r.SearchTags)
        //        .ToListAsync();

        //    if (!restaurants.Any()) return false;
        //    foreach (var r in restaurants)
        //    {
        //        if (dto.Description != null)
        //            r.Description = dto.Description;

        //        if (dto.CuisineTypeId.HasValue)
        //            r.CuisineTypeId = dto.CuisineTypeId;
        //        if (dto.AddSearchTags != null)
        //        {
        //            foreach (var tag in dto.AddSearchTags)
        //            {
        //                if (!r.SearchTags.Any(t => t.TagName == tag))
        //                {
        //                    r.SearchTags.Add(new RestaurantTag { TagName = tag });
        //                }
        //            }
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> DeleteTagAsync(int id, List<string> tagNames)
        //{
        //    var restaurant = await _context.Restaurants
        //        .Include(r => r.SearchTags)
        //        .FirstOrDefaultAsync(r => r.RestaurantId == id);

        //    if (restaurant == null) return false;

        //    // restaurant.SearchTags.Remove(t => tagNames.Contains(t.TagName));

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> UpdateOperatingHoursAsync(int restaurantId, List<RestaurantHourDto> hours)
        //{
        //    var restaurant = await _context.Restaurants
        //        .Include(r => r.OperatingHours)
        //        .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

        //    if (restaurant == null) return false;

        //    _context.RestaurantHours.RemoveRange(restaurant.OperatingHours);

        //    restaurant.OperatingHours = hours.Select(h => new RestaurantHour
        //    {
        //        DayOfWeek = h.DayOfWeek,
        //        OpenTime = TimeSpan.Parse(h.OpenTime),
        //        CloseTime = TimeSpan.Parse(h.CloseTime)
        //    }).ToList();
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<IEnumerable<RestaurantDto>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            var totalCount = await _context.Restaurants.CountAsync();

            var restaurants = await _context.Restaurants
                //.Include(r => r.CuisineType)
                //.Include(r => r.OperatingHours)
                //.Include(r => r.SearchTags)
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

        private RestaurantDto MapToDto(Restaurant r)
        {
            //var now = DateTime.Now;
            //var today = now.DayOfWeek;
            //var time = now.TimeOfDay;

            //var todaysHours = r.OperatingHours.FirstOrDefault(h => h.DayOfWeek == today);
            //bool isOpen = todaysHours != null &&
            //    (todaysHours.OpenTime <= todaysHours.CloseTime
            //    ? time >= todaysHours.OpenTime && time <= todaysHours.CloseTime
            //    : time >= todaysHours.OpenTime || time <= todaysHours.CloseTime);

            return new RestaurantDto
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                Description = r.Description,
                //CuisineTypeId = r.CuisineTypeId,
                //SearchTags = string.IsNullOrEmpty(r.SearchTagsJson) ? null : JsonSerializer.Deserialize<List<string>>(r.SearchTagsJson),
                //Address = r.Address == null ? null : new AddressDto
                //{
                //    Line1 = r.Address.Line1,
                //    Line2 = r.Address.Line2,
                //    City = r.Address.City,
                //    State = r.Address.State,
                //    PostalCode = r.Address.PostalCode,
                //    Country = r.Address.Country
                //}
                //CuisineTypeId = r.CuisineTypeId ?? 0,
                //Cuisine = r.CuisineType!,
                //Price = r.Price,
                //ImageUrl = r.ImageUrl,
                //Address = r.Address,
                //IsCurrentlyOpen = isOpen,
                //OperatingHours = r.OperatingHours
                //.OrderBy(h => h.DayOfWeek)
                //.Select(h => new RestaurantHourDto
                //{
                //    DayOfWeek = h.DayOfWeek,
                //    OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                //    CloseTime = h.CloseTime.ToString(@"hh\:mm")
                //}).ToList()
            };
        }
    }
}
