using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IOperatingHoursService
    {
        Task<List<OperatingHourDto>> GetByRestaurantAsync(int restauarantId);
        Task<OperatingHourDto> CreateAsync(OperatingHourCreateDto dto);
        Task<bool> UpdateAsync(int id, OperatingHourUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
    public class OperatingHoursService : IOperatingHoursService
    {
        private readonly DeliveryAppDbContext _context;
        public OperatingHoursService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperatingHourDto>> GetByRestaurantAsync(int restauarantId) {
var results = await _context.OperatingHours
                .Where(x=>x.RestaurantId == restauarantId)
                .ToListAsync();

            return results.Select(x => new OperatingHourDto
            {
                OperatingHourId = x.OperatingHourId,
                DayOfWeek = (int)x.DayOfWeek,
                DayName = x.DayOfWeek.ToString(),
                //IsClosed = x.IsClosed,
                OpenTime = TimeOnly.FromTimeSpan(x.OpenTime),
                CloseTime = TimeOnly.FromTimeSpan(x.CloseTime),
                RestaurantId = x.RestaurantId
            }).ToList();
        }

        public async Task<OperatingHourDto> CreateAsync(OperatingHourCreateDto dto)
        {
            var results = new OperatingHours
            {
                RestaurantId = dto.RestaurantId,
                DayOfWeek = (DayOfWeek)dto.DayOfWeek,
                //IsClosed = dto.IsClosed,
                OpenTime = dto.OpenTime.HasValue ? TimeSpan.FromHours(dto.OpenTime.Value.Hour).Add(TimeSpan.FromMinutes(dto.OpenTime.Value.Minute)) : TimeSpan.Zero,
                CloseTime = dto.CloseTime.HasValue ? TimeSpan.FromHours(dto.CloseTime.Value.Hour).Add(TimeSpan.FromMinutes(dto.CloseTime.Value.Minute)) : TimeSpan.Zero
            };
            _context.OperatingHours.Add(results);
            await _context.SaveChangesAsync();

            return new OperatingHourDto
            {
                OperatingHourId = results.OperatingHourId,
                DayOfWeek = (int)results.DayOfWeek,
                DayName = results.DayOfWeek.ToString(),
                //IsClosed = results.IsClosed,
                OpenTime = TimeOnly.FromTimeSpan(results.OpenTime),
                CloseTime = TimeOnly.FromTimeSpan(results.CloseTime),
                RestaurantId = results.RestaurantId
            };
        }
        public async Task<bool> UpdateAsync(int id, OperatingHourUpdateDto dto) { 
        var results = await _context.OperatingHours.FirstOrDefaultAsync(x => x.OperatingHourId == id);
            if (results == null) return false;
           // results.DayOfWeek = dto.DayOfWeek;
           // results.IsClosed = dto.IsClosed;
           // results.OpenTime = dto.OpenTime;
           // results.CloseTime = dto.CloseTime;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;

            var results = await _context.OperatingHours.FirstOrDefaultAsync(x => x.OperatingHourId == id);
            if (results == null) return false;
            _context.OperatingHours.Remove(results);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
