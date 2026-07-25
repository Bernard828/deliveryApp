using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

    
}