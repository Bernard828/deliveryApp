import { OrderItemDto } from "./order";

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
  name: string;
  description?: string;
  price: number;
 // section: MenuSection;
  //entreeSubSection?: EntreeSubSection;
  //drinkSubSection?: DrinkSubSection;
  imageUrl: string;
  isActive: boolean;
  searchTags: string[];

  restaurantId: number;

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
