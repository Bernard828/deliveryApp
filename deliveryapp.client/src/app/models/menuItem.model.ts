export interface MenuItem {
  menuItemId: number;
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
  category: MenuCategory;
}

export type MenuCategory =
  | 'Apps'
  | 'SoupSalad'
  | 'Sandwich'
  | 'Entrees'
  | 'Kids'
  | 'Dessert';

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
