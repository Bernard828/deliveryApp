using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IRestaurantTagService
    {
        Task<List<RestaurantTagDto>> GetAllAsync();
        Task<List<RestaurantTagDto>> GetByRestaurantAsync(int restaurantId);
        Task<RestaurantTagDto> CreateAsync(RestaurantTagCreateDto dto);
        Task<bool> UpdateAsync(int id, RestaurantTagUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AssignToRestaurantAsync(RestaurantTagAssignmentDto dto);
    }
    public class RestaurantTagService : IRestaurantTagService
    {
        private readonly DeliveryAppDbContext _context;
        public RestaurantTagService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantTagDto>> GetAllAsync()
        {
            var tags = await _context.ResturantTags
                .AsNoTracking()
                .OrderBy(t => t.TagName)
                .Select(t => new RestaurantTagDto
                {
                    RestaurantTagId = t.RestaurantTagId,
                    Name = t.TagName
                }).ToListAsync();

            return (tags);
        }

        public async Task<List<RestaurantTagDto>> GetByRestaurantAsync(int restaurantId)
        {
            if (restaurantId <= 0) return new List<RestaurantTagDto>();
            var tags = await _context.ResturantTags
                .AsNoTracking()
                .Where(a => a.RestaurantId == restaurantId)
                .Select(a => new RestaurantTagDto
                {
                    RestaurantTagId = a.RestaurantTagId,
                    Name = a.TagName
                })
                .OrderBy(t => t.Name)
                .ToListAsync();
            return tags;
        }

        public async Task<RestaurantTagDto?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;

            return await _context.ResturantTags
                .AsNoTracking()
                .Where(x => x.RestaurantTagId == id)
                .Select(x => new RestaurantTagDto
                {
                    RestaurantTagId = x.RestaurantTagId,
                    Name = x.TagName
                })
                .OrderBy(x => x.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<RestaurantTagDto> CreateAsync(RestaurantTagCreateDto dto)
        {
            var tag = new RestaurantTag
            {
                TagName = dto.Name.Trim()
            };

            _context.ResturantTags.Add(tag);

            await _context.SaveChangesAsync();

            return new RestaurantTagDto
            {
                RestaurantTagId = tag.RestaurantTagId,
                Name = tag.TagName
            };

        }

        public async Task<bool> UpdateAsync(int id, RestaurantTagUpdateDto dto)
        {
            var tag = await _context.ResturantTags.FindAsync(id);
            if (tag == null) return false;

            tag.TagName = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            var tag = await _context.ResturantTags
                    .FirstOrDefaultAsync(t => t.RestaurantTagId == id);

            if (tag == null) return false;

            _context.ResturantTags.Remove(tag);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignToRestaurantAsync(RestaurantTagAssignmentDto dto)
        {
            return true;
        }
    }
}
