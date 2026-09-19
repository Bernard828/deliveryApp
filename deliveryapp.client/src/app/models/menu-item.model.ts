export interface MenuItemDto {
  menuItemId: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  isActive: boolean;

  restaurantId: number;
  menuItemTagIds: number[];
}
export interface MenuItemCreateDto {
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  isActive: boolean;

  restaurantId: number;

  menuItemTagIds: number[];
}

export interface MenuItemUpdateDto {
  menuItemId: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  isActive: boolean;

  restaurantId: number;
  menuItemTagIds: number[];
}

export type MenuSection =
  | 'Appetizers'
  | 'SoupAndSalad'
  | 'Sandwiches'
  | 'Entrees'
  | 'Kids'
  | 'Desserts'
  | 'Drinks';

export type EntreeSubSection = 'Standard' | 'Vegetarian' | 'Vegan';
export type DrinkSubSection = 'NonAlcoholic' | 'Alcoholic';
