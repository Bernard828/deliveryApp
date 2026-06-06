using deliveryApp.Server.Models;

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
    public class MenuItemService
    {
    }
}
