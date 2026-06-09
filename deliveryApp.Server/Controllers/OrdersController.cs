using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace deliveryApp.Server.Controllers
{
    public class OrdersController : BaseApiController
    {
        //create new order
        //update order status (e.g., pending, accepted, in transit, delivered)
        //assign driver to order
        //fetch order history for customers and drivers

        public readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var order = await _orderService.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetDetails), new
            {
                id = order.OrderId
            }, order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetDetails(int id)
        {
            var details = await _orderService.GetOrderDetailsByIdAsync(id);
            if (details == null) return NotFound($"OrderId {id} not dfound");
            return Ok(details);
        }

        [HttpPut("status")]
        public async Task<ActionResult> UpdateStatus([FromBody] UpdateOrderStatusDto dto)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(dto);
            if (!updated) return NotFound($"Order {dto.OrderId} not found");
            return NoContent();
        }

        [HttpPut("assign-driver")]
        public async Task<IActionResult> AssignDriver([FromBody] AssignDriverDto dto)
        {
            var assigned = await _orderService.AssignDriverAsync(dto);
            if (!assigned) return BadRequest();
            return NoContent();
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<CustomerOrderHistoryDto>> GetCustomerHistory(int customerId)
        {
            return Ok(await _orderService.GetCustomerHistoryAsync(customerId));

        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<RestaurantOrderHistoryDto>> GetRestaurantHistory(int restaurantId)
        {
            return Ok(await _orderService.GetRestaurantHistoryAsync(restaurantId));
        }

        [HttpGet("driver/{driverId}")]
        public async Task<ActionResult<DriverOrderHistoryDto>> GetDriverOrderHistory(int driverId)
        {
            return Ok(await _orderService.GetDriverHistoryAsync(driverId));
        }
    }
}
