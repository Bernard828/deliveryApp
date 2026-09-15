using deliveryApp.Server.Data;
using deliveryApp.Server.DTOs;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAsync(int restaurantId, AddressCreateDto dto);
        Task<bool> UpdateAsync(int restaurantId, AddressUpdateDto dto);
        Task<bool> DeleteAsync(int restaurantId);
    }
    public class AddressService : IAddressService
    {
        private readonly DeliveryAppDbContext _context;
        public AddressService(DeliveryAppDbContext context)
        {
            _context = context;
        }
        public async Task<AddressDto> CreateAsync(int restaurantId, AddressCreateDto dto)
        {

            var address = new Address
            {
                Line1 = dto.Line1,
                Line2 = dto.Line2,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                RestaurantId = restaurantId
            };
            _context.Addresses.Add(address);

            await _context.SaveChangesAsync();

            return new AddressDto
            {
                AddressId = address.AddressId,
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }

        public async Task<bool> UpdateAsync(int restaurantId, AddressUpdateDto dto)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.RestaurantId == restaurantId);
            if (address == null) return false;

            address.Line1 = dto.Line1;
            address.Line2 = dto.Line2;
            address.City = dto.City;
            address.State = dto.State;
            address.PostalCode = dto.PostalCode;
            address.Country = dto.Country;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int restaurantId)
        {
            if (restaurantId <= 0) return false;

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.RestaurantId == restaurantId);

            if (address == null) return false;

            _context.Addresses.Remove(address);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
