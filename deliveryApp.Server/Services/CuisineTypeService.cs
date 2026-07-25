//using deliveryApp.Server.Data;
//using deliveryApp.Server.Models;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace deliveryApp.Server.Services
//{
//    public interface ICuisineTypeService
//    {
//        Task<(IEnumerable<CuisineType> CuisineTypes, int TotalCount)> GetAllAsync();
//        Task<CuisineType> CreateAsync(CuisineTypeCreateDto dto);
//        Task<bool> UpdateAsync(CuisineTypeUpdateDto dto);
//    }
//    public class CuisineTypeService : ICuisineTypeService
//    {
//        private readonly DeliveryAppDbContext _context;
//        public CuisineTypeService(DeliveryAppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<(IEnumerable<CuisineType> CuisineTypes, int TotalCount)> GetAllAsync()
//        {
//            var totalCount = await _context.CuisineTypes.CountAsync();
//            var results = await _context.CuisineTypes.ToListAsync();
//            //var page = 10; // Example page size
//            return (results, totalCount);
//        }
//        public async Task<CuisineType> CreateAsync(CuisineTypeCreateDto dto)
//        {
//            var cuisine = new CuisineType { Name = dto.Name };
//            _context.CuisineTypes.Add(cuisine);
//            await _context.SaveChangesAsync();
//            return cuisine;
//        }

//        public async Task<bool> UpdateAsync(CuisineTypeUpdateDto dto)
//        {
//            var cuisine = await _context.CuisineTypes.FindAsync(dto.CuisineTypeId);
//            if (cuisine == null) return false;

//            cuisine.Name = dto.Name;
//            await _context.SaveChangesAsync();
//            return true;
//        }
//    }
//}
