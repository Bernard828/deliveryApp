namespace deliveryApp.Server.Models
{
    public class Order
    {
        //Database entity
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public Guid DriverId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation properties
        public virtual User Customer { get; set; } = null!;
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual User? Driver { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
  
    public class OrderItem
    {
        //Database entity
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual MenuItem MenuItem { get; set; } = null!;
    }

    public class CreateOrderDto
    {
        //Input Data Transfer Object (Request)
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }   
 
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class AssignDriverDto
    {
        public int OrderId { get; set; }
        public Guid DriverId { get; set; }
    }

    public class OrderDto
    {
        //Output Data Transfer Object (Response)
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public Guid? DriverId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public Guid? DriverId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public Guid? DriverId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItemDetailsDto> OrderItems { get; set; } = new List<OrderItemDetailsDto>();
    }

    public class OrderItemDetailsDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomerOrderHistoryDto
    {
        public int CustomerId { get; set; }
        public List<OrderSummaryDto> Orders { get; set; } = new List<OrderSummaryDto>();
    }

    public class RestaurantOrderHistoryDto
    {
        public int RestaurantId { get; set; }
        public List<OrderSummaryDto> Orders { get; set; } = new List<OrderSummaryDto>();
    }

    public class DriverOrderHistoryDto
    {
        public Guid DriverId { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalEarnings { get; set; }
        public List<OrderSummaryDto> Orders { get; set; } = new List<OrderSummaryDto>();
    }

    public class DriverEarningsDto
    {
        public Guid DriverId { get; set; }
        public decimal TotalEarnings { get; set; }
    }

    //public class StatusUpdateDto
    //{
    //    public int OrderId { get; set; }
    //    public string Status { get; set; } = string.Empty;
    //}
    //public class AssignDriverRequestDto
    //{
    //    public int OrderId { get; set; }
    //    public int DriverId { get; set; }
    //}
    //public class CreateOrderRequestDto
    //{
    //    public int CustomerId { get; set; }
    //    public int RestaurantId { get; set; }
    //    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    //}
}
