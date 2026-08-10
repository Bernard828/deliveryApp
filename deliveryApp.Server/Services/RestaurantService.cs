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
        Task<List<RestaurantDto>> GetAllAsync();
        Task<bool> DeleteRestaurantAsync(int id);
        //Task<bool> UpdateOperatingHoursAsync(int restaurantId, List<RestaurantHourDto> hours);
        //Task<IEnumerable<RestaurantDto>> GetPagedAsync(int page, int pageSize);
        Task<PagedResultDto<RestaurantDto>> GetPagedAsync(int page, int pageSize, bool? isActive);

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
                //ImageUrl=dto.ImageUrl,
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
            restaurant.Description = dto.Description;
            //restaurant.CuisineTypeId = dto.CuisineTypeId;
            // restaurant.ImageUrl = dto.ImageUrl;
            // restaurant.Address = dto.Address;

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

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<RestaurantDto>> GetPagedAsync(int page, int pageSize, bool? isActive)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Restaurants.AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(r => r.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var restaurants = await query
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDto<RestaurantDto>
            {
                Items = restaurants.Select(MapToDto).ToList(),
                TotalCount = totalCount
            };
        }

        private RestaurantDto MapToDto(Restaurant r)
        {
            return new RestaurantDto
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                Description = r.Description,
                IsActive=r.IsActive,
                IsCurrentlyOpen=true,
                ImageUrl="https://unsplash.com",
                //Address= r.Address
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
