import { MenuItemSearchDto } from "./menuItem.model";

export interface RestaurantDto {
  restaurantId: number;
  name: string;
  description:string;
  cuisineTypeId: number;
  cuisine: CuisineType;
  price: number;
  imageUrl: string;
  address: string;
  isCurrentlyOpen: boolean;
  operatingHours: RestaurantHourDto[];
}

export interface RestaurantHourDto {
  dayOfWeek: number;
  openTime: string;
  closeTime: string;
}
export interface RestaurantSearchDto {
  restaurantId: number;
  name: string;
  description: string;
  cuisineTypeId: number;
  menuItems: MenuItemSearchDto[];
}
export interface RestaurantCreateDto {
  name: string;
  description: string;
  searchTags: string[];
  cuisineTypeId: number | null;
}
export interface CuisineType {
  cuisineTypeId: number;
  name: string;
}
