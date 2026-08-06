export interface OrderItemDto {
  menuItemId: number;
  quantity: number;
  price: number;
  name?: string;
}

export interface CreateOrderDto {
  customerId: number;
  restaurantId: number;
  orderItems: OrderItemDto[];
}
export interface CreateOrderDto {
  customerId: number;
  restaurantId: number;
  orderItems: OrderItemDto[];
}
