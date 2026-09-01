using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveryApp.Server.Services
{
    public interface IMenuItemService
    {
        Task<MenuItemDto> CreateAsync(MenuItemCreateDto dto);
        Task<bool> UpdateAsync(int id, MenuItemUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<MenuItemDto?> GetByIdAsync(int id);
        Task<IEnumerable<MenuItemSearchDto>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItemSearchDto>> SearchAsync(string query);
        Task<bool> UpdateTagsAsync(int menuItemId, List<string> tags);
        Task<bool> DeleteTagsAsync(int menuItemId, List<string> tags);
        Task<IEnumerable<MenuItemSearchDto>> GetPagedAsync(int page, int pageSize);
        Task<bool> BatchUpdateAsync(List<MenuItemUpdateDto> items);
    }

    public class MenuItemService : IMenuItemService
    {
        private readonly DeliveryAppDbContext _context;

        public MenuItemService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItemDto> CreateAsync(MenuItemCreateDto dto)
        {
            var item = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                RestaurantId = dto.RestaurantId,
                SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>()
            };

            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return MapToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, MenuItemUpdateDto dto)
        {
            if (id != dto.MenuItemId) return false;

            var item = await _context.MenuItems
                .Include(m => m.SearchTags)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (item == null) return false;

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.ImageUrl = dto.ImageUrl;
            item.RestaurantId = dto.RestaurantId;

            _context.RemoveRange(item.SearchTags);
            item.SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>();

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.MenuItems.AnyAsync(e => e.MenuItemId == id)) return false;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return false;

            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MenuItemDto?> GetByIdAsync(int id)
        {
            var item = await _context.MenuItems
                .Include(m => m.SearchTags)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            return item == null ? null : MapToDto(item);
        }

        public async Task<IEnumerable<MenuItemSearchDto>> GetByRestaurantIdAsync(int restaurantId)
        {
            var items = await _context.MenuItems
                .Include(m => m.SearchTags)
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();

            return items.Select(MapToSearchDto);
        }

        public async Task<IEnumerable<MenuItemSearchDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Enumerable.Empty<MenuItemSearchDto>();

            var lower = query.ToLower();
            var items = await _context.MenuItems
                .Include(m => m.SearchTags)
                .Where(m => m.Name.ToLower().Contains(lower) ||
                            m.Description.ToLower().Contains(lower) ||
                            m.SearchTags.Any(t => t.Name.ToLower().Contains(lower)))
                .ToListAsync();

            return items.Select(MapToSearchDto);
        }

        public async Task<bool> UpdateTagsAsync(int menuItemId, List<string> tags)
        {
            var item = await _context.MenuItems
                .Include(m => m.SearchTags)
                .FirstOrDefaultAsync(m => m.MenuItemId == menuItemId);

            if (item == null || tags == null) return false;

            _context.RemoveRange(item.SearchTags);
            item.SearchTags = tags.Select(t => new MenuItemTag { Name = t }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTagsAsync(int menuItemId, List<string> tags)
        {
            var item = await _context.MenuItems
                .Include(m => m.SearchTags)
                .FirstOrDefaultAsync(m => m.MenuItemId == menuItemId);

            if (item == null || tags == null) return false;

            var targetsToRemove = item.SearchTags.Where(t => tags.Contains(t.Name)).ToList();
            _context.RemoveRange(targetsToRemove);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MenuItemSearchDto>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var items = await _context.MenuItems
                .Include(m => m.SearchTags)
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return items.Select(MapToSearchDto);
        }

        public async Task<bool> BatchUpdateAsync(List<MenuItemUpdateDto> items)
        {
            if (items == null || !items.Any()) return false;

            var ids = items.Select(i => i.MenuItemId).ToList();
            var menuItems = await _context.MenuItems
                .Include(m => m.SearchTags)
                .Where(m => ids.Contains(m.MenuItemId))
                .ToListAsync();

            if (!menuItems.Any()) return false;

            foreach (var item in menuItems)
            {
                var dto = items.First(i => i.MenuItemId == item.MenuItemId);
                item.Name = dto.Name;
                item.Description = dto.Description;
                item.Price = dto.Price;
                item.ImageUrl = dto.ImageUrl;
                item.RestaurantId = dto.RestaurantId;

                _context.RemoveRange(item.SearchTags);
                item.SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private MenuItemDto MapToDto(MenuItem m)
        {
            return new MenuItemDto
            {
                MenuItemId = m.MenuItemId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ImageUrl = m.ImageUrl,
                RestaurantId = m.RestaurantId,
                SearchTags = m.SearchTags?.Select(t => t.Name).ToList() ?? new List<string>()
            };
        }

        private MenuItemSearchDto MapToSearchDto(MenuItem m)
        {
            return new MenuItemSearchDto
            {
                MenuItemId = m.MenuItemId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ImageUrl = m.ImageUrl,
                RestaurantId = m.RestaurantId,
                SearchTags = m.SearchTags?.Select(t => t.Name).ToList() ?? new List<string>()
            };
        }
    }
}


//using deliveryApp.Server.Controllers;
//using deliveryApp.Server.Data;
//using deliveryApp.Server.DTOs;
//using deliveryApp.Server.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace deliveryApp.Server.Services
//{
//    public class MenuItemService : BaseApiController
//    {
//        private readonly DeliveryAppDbContext _context;

//        public MenuItemService(DeliveryAppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<MenuItemDto> CreateAsync(MenuItemCreateDto dto)
//        {
//            var item = new MenuItem
//            {
//                Name = dto.Name,
//                Description = dto.Description,
//                Price = dto.Price,
//                ImageUrl = dto.ImageUrl,
//                RestaurantId = dto.RestaurantId,
//                SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>()
//            };

//            _context.MenuItems.Add(item);
//            await _context.SaveChangesAsync();
//            return MapToDto(item);
//        }

//        public async Task<bool> UpdateAsync(int id, MenuItemUpdateDto dto)
//        {
//            if (id != dto.MenuItemId) return false;

//            var item = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .FirstOrDefaultAsync(m => m.MenuItemId == id);

//            if (item == null) return false;

//            item.Name = dto.Name;
//            item.Description = dto.Description;
//            item.Price = dto.Price;
//            item.ImageUrl = dto.ImageUrl;
//            item.RestaurantId = dto.RestaurantId;

//            // Clear old child tag entity rows to cleanly rewrite the table lines
//            _context.RemoveRange(item.SearchTags);
//            item.SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>();

//            try
//            {
//                await _context.SaveChangesAsync();
//                return true;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!await _context.MenuItems.AnyAsync(e => e.MenuItemId == id)) return false;
//                throw;
//            }
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var item = await _context.MenuItems.FindAsync(id);
//            if (item == null) return false;

//            _context.MenuItems.Remove(item);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<MenuItemDto?> GetByIdAsync(int id)
//        {
//            var item = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .FirstOrDefaultAsync(m => m.MenuItemId == id);

//            return item == null ? null : MapToDto(item);
//        }

//        public async Task<IEnumerable<MenuItemSearchDto>> GetByRestaurantIdAsync(int restaurantId)
//        {
//            var items = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .Where(m => m.RestaurantId == restaurantId)
//                .ToListAsync();

//            return items.Select(MapToSearchDto);
//        }

//        public async Task<IEnumerable<MenuItemSearchDto>> SearchAsync(string query)
//        {
//            if (string.IsNullOrWhiteSpace(query)) return Enumerable.Empty<MenuItemSearchDto>();

//            var lower = query.ToLower();
//            var items = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .Where(m => m.Name.ToLower().Contains(lower) ||
//                            m.Description.ToLower().Contains(lower) ||
//                            m.SearchTags.Any(t => t.Name.ToLower().Contains(lower)))
//                .ToListAsync();

//            return items.Select(MapToSearchDto);
//        }

//        public async Task<bool> UpdateTagsAsync(int menuItemId, List<string> tags)
//        {
//            var item = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .FirstOrDefaultAsync(m => m.MenuItemId == menuItemId);

//            if (item == null || tags == null) return false;

//            _context.RemoveRange(item.SearchTags);
//            item.SearchTags = tags.Select(t => new MenuItemTag { Name = t }).ToList();

//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> DeleteTagsAsync(int menuItemId, List<string> tags)
//        {
//            var item = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .FirstOrDefaultAsync(m => m.MenuItemId == menuItemId);

//            if (item == null || tags == null) return false;

//            var targetsToRemove = item.SearchTags.Where(t => tags.Contains(t.Name)).ToList();
//            _context.RemoveRange(targetsToRemove);

//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<IEnumerable<MenuItemSearchDto>> GetPagedAsync(int page, int pageSize)
//        {
//            if (page < 1) page = 1;
//            if (pageSize < 1) pageSize = 10;

//            var items = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .OrderBy(m => m.Name)
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            return items.Select(MapToSearchDto);
//        }

//        public async Task<bool> BatchUpdateAsync(List<MenuItemUpdateDto> items)
//        {
//            if (items == null || !items.Any()) return false;

//            var ids = items.Select(i => i.MenuItemId).ToList();
//            var menuItems = await _context.MenuItems
//                .Include(m => m.SearchTags)
//                .Where(m => ids.Contains(m.MenuItemId))
//                .ToListAsync();

//            if (!menuItems.Any()) return false;

//            foreach (var item in menuItems)
//            {
//                var dto = items.First(i => i.MenuItemId == item.MenuItemId);
//                item.Name = dto.Name;
//                item.Description = dto.Description;
//                item.Price = dto.Price;
//                item.ImageUrl = dto.ImageUrl;
//                item.RestaurantId = dto.RestaurantId;

//                _context.RemoveRange(item.SearchTags);
//                item.SearchTags = dto.SearchTags?.Select(t => new MenuItemTag { Name = t }).ToList() ?? new List<MenuItemTag>();
//            }

//            await _context.SaveChangesAsync();
//            return true;
//        }

//        // Mapping Mappers
//        private MenuItemDto MapToDto(MenuItem m)
//        {
//            return new MenuItemDto
//            {
//                MenuItemId = m.MenuItemId,
//                Name = m.Name,
//                Description = m.Description,
//                Price = m.Price,
//                ImageUrl = m.ImageUrl,
//                RestaurantId = m.RestaurantId,
//                SearchTags = m.SearchTags?.Select(t => t.Name).ToList() ?? new List<string>()
//            };
//        }

//        private MenuItemSearchDto MapToSearchDto(MenuItem m)
//        {
//            return new MenuItemSearchDto
//            {
//                MenuItemId = m.MenuItemId,
//                Name = m.Name,
//                Description = m.Description,
//                Price = m.Price,
//                ImageUrl = m.ImageUrl,
//                RestaurantId = m.RestaurantId,
//                SearchTags = m.SearchTags?.Select(t => t.Name).ToList() ?? new List<string>()
//            };
//        }
//    }
//}
