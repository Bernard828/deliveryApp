using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveryApp.Server.Services
{
    public interface IRestaurantService
    {
        //Task<IEnumerable<RestaurantDto>> GetAllAsync(bool? activeOnly, int? cuisineTypeId, CancellationToken cancellationToken = default);
        Task<PagedResultDto<RestaurantDto>> GetPagedAsync(int page, int pageSize, bool? isActive, CancellationToken cancellationToken = default);
        Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, RestaurantUpdateDto dto, CancellationToken cancellationToken = default);
        Task<bool> ToggleActiveStatusAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly DeliveryAppDbContext _context;

        public RestaurantService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        // Get All Restaurants with optional filters
        //public async Task<IEnumerable<RestaurantDto>> GetAllAsync(bool? activeOnly, int? cuisineTypeId, CancellationToken cancellationToken = default)
        //{
        //    var query = _context.Restaurants
        //        .AsNoTracking()
        //        .Include(r => r.CuisineType)
        //        .Include(r => r.Address)
        //        .Include(r => r.OperatingHours)
        //        .Include(r => r.SearchTags)
        //        .AsQueryable();

        //    // Default filter returns only active restaurants
        //    if (activeOnly ?? true)
        //    {
        //        query = query.Where(r => r.IsActive);
        //    }

        //    // Filter by cuisine type if provided
        //    if (cuisineTypeId.HasValue)
        //    {
        //        query = query.Where(r => r.CuisineTypeId == cuisineTypeId.Value);
        //    }


        //    return await query
        //        .OrderBy(r => r.Name)
        //        .Select(r => new RestaurantDto
        //        {
        //            RestaurantId = r.RestaurantId,
        //            Name = r.Name,
        //            Description = r.Description,
        //            IsActive = r.IsActive,
        //            CuisineTypeId = r.CuisineTypeId,
        //            CuisineTypeName = r.CuisineType != null
        //            ? r.CuisineType.Name
        //            : null,
        //            ImageUrl = r.ImageUrl,
        //            Address = r.Address == null
        //            ? null
        //            : new AddressDto
        //            {
        //                AddressId = r.Address.AddressId,
        //                Line1 = r.Address.Line1,
        //                Line2 = r.Address.Line2,
        //                City = r.Address.City,
        //                State = r.Address.State,
        //                Country = r.Address.Country,
        //                PostalCode = r.Address.PostalCode
        //            }
        //        })
        //        .ToListAsync(cancellationToken);
        //}

        // Paged Results
        public async Task<PagedResultDto<RestaurantDto>> GetPagedAsync(
            int page,
            int pageSize,
            bool? isActive,
            CancellationToken cancellationToken = default)
        {
            //Default values for page and pageSize if they are less than 1
            if (page < 1)
            {
                page = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            if (pageSize > 100)
            {
                pageSize = 100;
            }

            var query = _context.Restaurants
                .AsNoTracking()
                //.Include(r => r.CuisineType)
                //.Include(r => r.Address)
                //.Include(r => r.OperatingHours)
                //.Include(r => r.SearchTags)
                .AsSplitQuery() //avoid duplicate items
                .AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(r =>
                r.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var restaurants = await query
                .OrderBy(r => r.Name)
                .ThenBy(r => r.RestaurantId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RestaurantDto
                {
                    RestaurantId = r.RestaurantId,
                    Name = r.Name,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CuisineTypeId = r.CuisineTypeId,

                    CuisineTypeName = r.CuisineType != null
                    ? r.CuisineType.Name
                    : null,

                    ImageUrl = r.ImageUrl,

                    Address = r.Address == null
                    ? null
                    : new AddressDto
                    {
                        AddressId = r.Address.AddressId,
                        Line1 = r.Address.Line1,
                        Line2 = r.Address.Line2,
                        City = r.Address.City,
                        State = r.Address.State,
                        PostalCode = r.Address.PostalCode,
                        Country = r.Address.Country
                    },

                    SearchTags = r.SearchTags
                    .Select(t => t.TagName)
                    .ToList(),

                    OperatingHours = r.OperatingHours
                    .Select(h => new OperatingHoursDto
                    {
                        DayOfWeek = h.DayOfWeek,
                        OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                        CloseTime = h.CloseTime.ToString(@"hh\:mm")
                    })
                    .OrderBy(h => h.DayOfWeek)
                    .ToList(),

                    MenuItems = r.MenuItems
                    .Select(m => new MenuItemDto
                    {
                        MenuItemId = m.MenuItemId,
                        Name = m.Name,
                        Description = m.Description,
                        Price = m.Price,
                        ImageUrl = m.ImageUrl,
                        RestaurantId = m.RestaurantId,

                        SearchTags = m.SearchTags
                        .Select(t => t.Name)
                        .ToList()
                    })
                    .ToList()
                })
                .ToListAsync(cancellationToken);

            return new PagedResultDto<RestaurantDto>
            {
                Items = restaurants,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        //Get By ID
        public async Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Restaurants
                .AsNoTracking()
                .Where(r => r.RestaurantId == id)
               .Select(r => new RestaurantDto
               {
                   RestaurantId = r.RestaurantId,
                   Name = r.Name,
                   Description = r.Description,
                   IsActive = r.IsActive,
                   CuisineTypeId = r.CuisineTypeId,

                   CuisineTypeName = r.CuisineType == null
                   ? null
                   : r.CuisineType.Name,

                   ImageUrl = r.ImageUrl,

                   Address = r.Address == null
                   ? null
                   : new AddressDto
                   {
                       AddressId = r.Address.AddressId,
                       Line1 = r.Address.Line1,
                       Line2 = r.Address.Line2,
                       City = r.Address.City,
                       State = r.Address.State,
                       PostalCode = r.Address.PostalCode,
                       Country = r.Address.Country
                   },

                   SearchTags = r.SearchTags
                   .Select(t => t.TagName)
                   .ToList(),

                   OperatingHours = r.OperatingHours
                   .Select(h => new OperatingHoursDto
                   {
                       DayOfWeek = h.DayOfWeek,
                       OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                       CloseTime = h.CloseTime.ToString(@"hh\:mm")
                   })
                   .OrderBy(h => h.DayOfWeek)
                   .ToList(),

                   MenuItems = r.MenuItems
                   .Select(m => new MenuItemDto
                   {
                       MenuItemId = m.MenuItemId,
                       Name = m.Name,
                       Description = m.Description,
                       Price = m.Price,
                       ImageUrl = m.ImageUrl,
                       RestaurantId = m.RestaurantId,

                       SearchTags = m.SearchTags
                       .Select(t => t.Name)
                       .ToList()
                   })
                   .ToList()
               })
               .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<RestaurantDto> CreateAsync(RestaurantCreateDto dto, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var restaurant = new Restaurant
            {
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim(),
                IsActive = dto.IsActive,
                CuisineTypeId = dto.CuisineTypeId,
                ImageUrl = dto.ImageUrl.Trim(),

                Address = dto.Address == null
                ? null
                : new Address
                {
                    AddressId = dto.Address.AddressId,
                    Line1 = dto.Address?.Line1.Trim() ?? string.Empty,
                    Line2 = dto.Address?.Line2.Trim() ?? string.Empty,
                    City = dto.Address?.City.Trim() ?? string.Empty,
                    State = dto.Address?.State.Trim() ?? string.Empty,
                    PostalCode = dto.Address?.PostalCode.Trim() ?? string.Empty,
                    Country = dto.Address?.Country.Trim() ?? string.Empty
                },

                SearchTags = dto.SearchTags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => new RestaurantTag
                {
                    TagName = t.Trim()
                })
                .ToList(),

                OperatingHours = dto.OperatingHours
                .Select(h => new OperatingHours
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = TimeSpan.Parse(h.OpenTime),
                    CloseTime = TimeSpan.Parse(h.CloseTime)
                }).ToList()
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync(cancellationToken);

            return MapToDto(restaurant);
        }

        public async Task<bool> UpdateAsync(
            int id, RestaurantUpdateDto dto, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(dto);

            if (id <= 0)
            {
                return false;
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.Address)
                .Include(r => r.OperatingHours)
                .Include(r => r.SearchTags)
                .FirstOrDefaultAsync(r => r.RestaurantId == id, cancellationToken);

            if (restaurant == null)
            {
                return false;
            }

            restaurant.Name = dto.Name.Trim();
            restaurant.Description = dto.Description.Trim();
            restaurant.IsActive = dto.IsActive;
            restaurant.CuisineTypeId = dto.CuisineTypeId;
            restaurant.ImageUrl = dto.ImageUrl.Trim();

            // Update Owned Address values safely
            if (dto.Address == null)
            {
                if (restaurant.Address == null)
                {
                    restaurant.Address = new Address();
                }
                restaurant.Address.AddressId = dto.Address.AddressId;
                restaurant.Address.Line1 = dto.Address?.Line1.Trim() ?? string.Empty;
                restaurant.Address.Line2 = dto.Address?.Line2.Trim() ?? string.Empty;
                restaurant.Address.City = dto.Address?.City.Trim() ?? string.Empty;
                restaurant.Address.State = dto.Address?.State.Trim() ?? string.Empty;
                restaurant.Address.PostalCode = dto.Address?.PostalCode.Trim() ?? string.Empty;
                restaurant.Address.Country = dto.Address?.Country.Trim() ?? string.Empty;
            }
            // Clear database trackers before rebuilding lists to avoid orphan tracking bugs
            _context.ResturantTags.RemoveRange(restaurant.SearchTags);

            restaurant.SearchTags = dto.SearchTags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => new RestaurantTag
                {
                    TagName = t.Trim()
                })
                .ToList();

            // Replace OperatingHours with new values from DTO
            if (restaurant.OperatingHours != null)
            {
                _context.OperatingHours.RemoveRange(restaurant.OperatingHours);
            }
            restaurant.OperatingHours = dto.OperatingHours
                .Select(h => new OperatingHours
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = TimeSpan.Parse(h.OpenTime),
                    CloseTime = TimeSpan.Parse(h.CloseTime)
                }).ToList();

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Toggle Active Status
        public async Task<bool> ToggleActiveStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) { return false; }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id, cancellationToken);

            if (restaurant == null) { return false; }

            restaurant.IsActive = !restaurant.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) { return false; }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id, cancellationToken);

            if (restaurant == null) { return false; }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private RestaurantDto MapToDto(Restaurant restaurant)
        {
            var now = DateTime.Now;
            var currentDay = now.DayOfWeek;
            var currentTime = now.TimeOfDay;

            bool isOpenNow = restaurant.OperatingHours != null && restaurant.OperatingHours.Any(h =>
                h.DayOfWeek == currentDay &&
                currentTime >= h.OpenTime &&
                currentTime <= h.CloseTime);

            var dto = new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                Description = restaurant.Description,
                IsActive = restaurant.IsActive,
                CuisineTypeId = restaurant.CuisineTypeId,
                CuisineTypeName = restaurant.CuisineType?.Name ?? string.Empty,
                ImageUrl = restaurant.ImageUrl,
                //IsCurrentlyOpen = isOpenNow,
                Address = restaurant.Address == null
                ? null
                : new AddressDto
                {
                    AddressId = restaurant.Address.AddressId,
                    Line1 = restaurant.Address.Line1 ?? string.Empty,
                    Line2 = restaurant.Address.Line2 ?? string.Empty,
                    City = restaurant.Address.City ?? string.Empty,
                    State = restaurant.Address.State ?? string.Empty,
                    PostalCode = restaurant.Address.PostalCode ?? string.Empty,
                    Country = restaurant.Address.Country ?? string.Empty
                },

                SearchTags = restaurant.SearchTags?
                .Select(t => t.TagName)
                .ToList() ?? new List<string>(),

                OperatingHours = restaurant.OperatingHours?
                .Select(h => new OperatingHoursDto
                {
                    DayOfWeek = h.DayOfWeek,
                    OpenTime = h.OpenTime.ToString(@"hh\:mm"),
                    CloseTime = h.CloseTime.ToString(@"hh\:mm")
                })
                .OrderBy(h => h.DayOfWeek)
                .ToList() ?? new List<OperatingHoursDto>()
            };

            return dto;
        }
    }
}
