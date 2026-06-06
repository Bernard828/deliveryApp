using deliveryApp.Server.Data;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetMenuByRestaurantIdAsync(int restaurantId);
        Task<MenuItem> GetByIdAsync(int id);
        Task<MenuItem> Createasync(MenuItem item);
        Task<bool> UpdateAsync(int id, MenuItem item);
        Task<bool> DeleteAsync(int id);
    }
    public class MenuItemService : IMenuItemService
    {
        private readonly DeliveryAppDbContext _context;

        public MenuItemService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        public async Task<MenuItem> CreateAsync(MenuItem item)
        {
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, MenuItem item)
        {
            if (id != item.MenuItemId) return false;
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
    }
}
