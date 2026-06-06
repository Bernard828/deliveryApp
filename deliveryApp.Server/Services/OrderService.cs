using deliveryApp.Server.Data;
using deliveryApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryApp.Server.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDetailsDto> GetOrderDetailsByIdAsync(int id);
        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto);
        Task<bool> AssignDriverAsync(AssignDriverDto assignDriverDto);
        Task<CustomerOrderHistoryDto> GetCustomerHistoryAsync(int customerId);
        Task<RestaurantOrderHistoryDto> GetRestaurantHistoryAsync(int restaurantId);
        Task<DriverOrderHistoryDto> GetDriverHistoryAsync(int driverId);
        Task<DriverEarningsDto> GetDriversEarningsAsync(int driverId);
    }
    public class OrderService : IOrderService
    {
        private readonly DeliveryAppDbContext _context;

        public OrderService(DeliveryAppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createdOrderDto)
        {
            var order = new Order
            {
                CustomerId = createdOrderDto.CustomerId,
                RestaurantId = createdOrderDto.RestaurantId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            decimal calculatedTotal = 0;

            foreach (var itemDto in createdOrderDto.OrderItems)
            {
                var menuItem = await _context.MenuItems.FindAsync(itemDto.MenuItemId);
                decimal price = menuItem?.Price ?? itemDto.Price;

                var orderItem = new OrderItem
                {
                    MenuItemId = itemDto.MenuItemId,
                    Quantity = itemDto.Quantity,
                    Price = price,
                };

                calculatedTotal += price * itemDto.Quantity;
                order.OrderItems.Add(orderItem);
            }
            order.TotalPrice = calculatedTotal;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                DriverId = order.DriverId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }

        public async Task<OrderDetailsDto> GetOrderDetailsByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null!;
            return new OrderDetailsDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                DriverId = order.DriverId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDetailsDto
                {
                    MenuItemId = oi.MenuItemId,
                    MenuItemName = oi.MenuItem?.Name ?? "Unknown Item",
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }

        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto)
        {
            var order = await _context.Orders.FindAsync(updateOrderStatusDto.OrderId);
            if (order == null) return false;

            order.Status = updateOrderStatusDto.Status; ;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignDriverAsync(AssignDriverDto assignDriverDto)
        {
            var order = await _context.Orders.FindAsync(assignDriverDto.OrderId);
            if (order == null) return false;

            //Verify entity has Driver Role
            var driverExists = await _context.Users.AnyAsync(u => u.UserId == assignDriverDto.DriverId);
            if (!driverExists) return false;

            order.DriverId = assignDriverDto.DriverId;
            order.Status = "Accepted By Driver";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerOrderHistoryDto> GetCustomerHistoryAsync(int customerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Select(o => MapToSummaryDto(o))
                .ToListAsync();

            return new CustomerOrderHistoryDto
            {
                CustomerId = customerId,
                Orders = orders
            };
        }

        public async Task<RestaurantOrderHistoryDto> GetRestaurantHistoryAsync(int resaturantId)
        {
            var orders = await _context.Orders
                .Where(o => o.RestaurantId == resaturantId)
                .Select(o => MapToSummaryDto(o))
                .ToListAsync();

            return new RestaurantOrderHistoryDto
            {
                RestaurantId = resaturantId,
                Orders = orders
            };
        }

        public async Task<DriverOrderHistoryDto> GetDriverHistoryAsync(int driverId)
        {
            var baseQuery = await _context.Orders
                .Where(o => o.DriverId == driverId)
                .ToListAsync();

            var orders = baseQuery.Select(o =>
            MapToSummaryDto(o)).ToList();
            decimal earnings = baseQuery
                .Where(o => o.Status == "Delivered")
                .Sum(o => o.TotalPrice);

            return new DriverOrderHistoryDto
            {
                DriverId = driverId,
                TotalOrders = orders.Count,
                TotalEarnings = earnings,
                Orders = orders
            };
        }

        public async Task<DriverEarningsDto> GetDriversEarningsAsync(int driverId)
        {
            decimal earnings = await _context.Orders
                .Where(o => o.DriverId == driverId && o.Status == "Delivered")
                .SumAsync(o => o.TotalPrice);

            return new DriverEarningsDto
            {
                DriverId = driverId,
                TotalEarnings = earnings
            };
        }

        private static OrderSummaryDto MapToSummaryDto(Order o) => new()
        {
            OrderId = o.OrderId,
            CustomerId = o.CustomerId,
            RestaurantId = o.RestaurantId,
            DriverId = o.DriverId,
            TotalPrice = o.TotalPrice,
            Status = o.Status,
            CreatedAt = o.CreatedAt
        };
    }
}