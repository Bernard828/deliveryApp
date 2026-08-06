import { CuisineType } from "./cuisine-type.model";
import { MenuItemDto } from "./menuItem.model";
import { RestaurantHourDto } from "./restaurant-hours.model";

export interface Restaurant {
  restaurantId: number;
  name: string;
  isActive: boolean;
  cuisineTypeId: number;
  operatingHours: RestaurantHourDto[];
  isCurrentlyOpen: boolean;
  description:string;
  cuisine: CuisineType;
  imageUrl: string;
  address: string;
}

export interface RestaurantDto {
  // restaurantId: number;
  // name: string;
  // description: string;
  // cuisineTypeId: number;
  // menuItems: MenuItemDto[];
  restaurantId: number;
  name: string;
  isActive: boolean;
  cuisineTypeId: number;
  operatingHours: RestaurantHourDto[];
  isCurrentlyOpen: boolean;
  description: string;
  cuisine: CuisineType;
  //price: number;
  imageUrl: string;
  address: string;
  menuItemId: number;
  menuItems: MenuItemDto[];
}
export interface RestaurantCreateDto {
  name: string;
  description: string;
  searchTags: string[];
  cuisineTypeId: number | null;
}

