using deliveryApp.Server.Data;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IMenuItemService
    {
        Task<MenuItem?> GetByIdAsync(int id);
        Task<IEnumerable<MenuItemSearchDto>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<MenuItemSearchDto>> SearchAsync(string query);
        Task<MenuItem> CreateAsync(MenuItemCreateDto dto);
        Task<bool> UpdateAsync(int id, MenuItemUpdateDto item);
        Task<bool> DeleteAsync(int id);
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

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
        }
        public async Task<IEnumerable<MenuItemSearchDto>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .Select(m => new MenuItemSearchDto
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Price = m.Price,
                    SearchTags = m.SearchTags,
                    RestaurantId = m.RestaurantId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItemSearchDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<MenuItemSearchDto>();
            var lower = query.ToLower();
            return await _context.MenuItems
                .Where(m =>
                m.Name.ToLower().Contains(lower) ||
                m.Description.ToLower().Contains(lower) ||
                m.SearchTags.ToLower().Contains(lower))
                .Select(m => new MenuItemSearchDto
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Price = m.Price,
                    SearchTags = m.SearchTags,
                    RestaurantId = m.RestaurantId
                }).ToListAsync();
        }

        public async Task<MenuItem> CreateAsync(MenuItemCreateDto dto)
        {
            var item = new MenuItem
            {
                Name = dto.Name,
                Price = dto.Price,
                SearchTags = dto.SearchTags,
                RestaurantId = dto.RestaurantId,
            };
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, MenuItemUpdateDto dto)
        {
            var item = await _context.MenuItems.FindAsync(dto.MenuItemId);
            if (id != dto.MenuItemId) return false;
            if (item == null) return false;

            item.Name = dto.Name;
            item.Price = dto.Price;
            item.SearchTags = dto.SearchTags;
            item.RestaurantId = dto.RestaurantId;

            _context.Entry(item).State = EntityState.Modified;

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

        public async Task<bool> UpdateTagsAsync(int menuItemId, List<string> tags)
        {
            var item = await _context.MenuItems.FindAsync(menuItemId);
            if (item == null) return false;
            item.SearchTags = string.Join(",", tags);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTagsAsync(int menuItemId, List<string> tags)
        {
            var item = await _context.MenuItems.FindAsync(menuItemId);
            if (item == null) return false;

            var currentTags = item.SearchTags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList();
            currentTags.RemoveAll(t => tags.Contains(t));

            item.SearchTags = string.Join(",", currentTags);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MenuItemSearchDto>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return await _context.MenuItems
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MenuItemSearchDto
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Price = m.Price,
                    SearchTags = m.SearchTags,
                    RestaurantId = m.RestaurantId
                }).ToListAsync();
        }

        public async Task<bool> BatchUpdateAsync(List<MenuItemUpdateDto> items)
        {
            var ids = items.Select(i => i.MenuItemId).ToList();
            var menuItems = await _context.MenuItems
                .Where(m => ids.Contains(m.MenuItemId)).ToListAsync();
            if (!menuItems.Any()) return false;
            foreach (var item in menuItems)
            {
                var dto = items.First(i => i.MenuItemId == item.MenuItemId);
                item.Name = dto.Name;
                item.Price = dto.Price;
                item.SearchTags = dto.SearchTags;
                item.RestaurantId = dto.RestaurantId;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
