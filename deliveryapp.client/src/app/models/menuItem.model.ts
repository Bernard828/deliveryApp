export interface MenuItem {
  menuItemId: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  restaurantId: number;
}
export interface MenuItemDto {
  menuItemId: number;
  restaurantId: number;
  name: string;
  description?: string;
  price: number;
  section: MenuSection;
  entreeSubSection?: EntreeSubSection;
  drinkSubSection?: DrinkSubSection;
  imageUrl: string;
  isActive: boolean;
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

export type MenuSection =
  | 'Apps'
  | 'SoupAndSalad'
  | 'Sandwiches'
  | 'Entrees'
  | 'Kids'
  | 'Desserts'
  | 'Drinks';

export type EntreeSubSection = 'Standard' | 'Vegetarian' | 'Vegan';
export type DrinkSubSection = 'NonAlcoholic' | 'Alcoholic';
