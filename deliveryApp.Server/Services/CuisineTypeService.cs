using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface ICuisineTypeService
    {
        Task<ActionResult<List<CuisineTypeDto>>> GetAllAsync();
        Task<CuisineTypeDto?> GetByIdAsync(int id);
        Task<CuisineTypeDto> CreateAsync(CuisineTypeCreateDto dto);
        Task<bool> UpdateAsync(int id, CuisineTypeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
    public class CuisineTypeService : ICuisineTypeService
    {
        private readonly DeliveryAppDbContext _context;
        public CuisineTypeService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<CuisineTypeDto>>> GetAllAsync()
        {
            var cuisineTypes = await _context.CuisineTypes
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CuisineTypeDto
                {
                    CuisineTypeId = c.CuisineTypeId,
                    Name = c.Name
                }).ToListAsync();

            return (cuisineTypes);
        }

        public async Task<CuisineTypeDto?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;

            return await _context.CuisineTypes
                .AsNoTracking()
                .Where(c => c.CuisineTypeId == id)
                .Select(c => new CuisineTypeDto
                {
                    CuisineTypeId = c.CuisineTypeId,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<CuisineTypeDto> CreateAsync(CuisineTypeCreateDto dto)
        {
            var cuisine = new CuisineType
            {
                Name = dto.Name.Trim()
            };

            _context.CuisineTypes.Add(cuisine);

            await _context.SaveChangesAsync();

            return new CuisineTypeDto
            {
                CuisineTypeId = cuisine.CuisineTypeId,
                Name = cuisine.Name
            };

        }
        public async Task<bool> UpdateAsync(int id, CuisineTypeUpdateDto dto)
        {
            var cuisine = await _context.CuisineTypes.FindAsync(dto.CuisineTypeId);
            if (cuisine == null) return false;

            cuisine.Name = dto.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            var cuisine = await _context.CuisineTypes
                    .FirstOrDefaultAsync(c => c.CuisineTypeId == id);

            if (cuisine == null) return false;

            _context.CuisineTypes.Remove(cuisine);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}