using deliveryApp.Server.Data;
using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace deliveryApp.Server.Services
{
    public interface ICuisineTypeService
    {
        Task<CuisineType> CreateAsync(CuisineTypeCreateDto dto);
        //Task<int> CreateAsync(CuisineTypeCreateDto dto);

        Task<bool> UpdateAsync(CuisineTypeUpdateDto dto);
    }
    public class CuisineTypeService : ICuisineTypeService
    {
        private readonly DeliveryAppDbContext _context;
        public CuisineTypeService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<CuisineType> CreateAsync(CuisineTypeCreateDto dto)
        {
            var cuisine = new CuisineType { Name = dto.Name };
            _context.CuisineTypes.Add(cuisine);
            await _context.SaveChangesAsync();
            return cuisine;
        }

        public async Task<bool> UpdateAsync(CuisineTypeUpdateDto dto)
        {
            var cuisine = await _context.CuisineTypes.FindAsync(dto.CuisineTypeId);
            if (cuisine == null) return false;

            cuisine.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
