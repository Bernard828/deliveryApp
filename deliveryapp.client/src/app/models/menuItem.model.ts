export interface MenuItem {
  menuItem: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  restaurantId: number;
}
export interface MenuItemSearchDto {
  menuItemId: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
}

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
