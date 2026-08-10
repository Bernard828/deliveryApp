import { Address, AddressDto } from "./address.model";
import { CuisineType } from "./cuisine-type.model";
import { MenuItemDto } from "./menuItem.model";
import { RestaurantHourDto } from "./restaurant-hours.model";

export interface Restaurant {
  restaurantId: number;
  name: string;
  description:string;
  isActive: boolean;
  cuisineTypeId: number;
  operatingHours: RestaurantHourDto[];
  isCurrentlyOpen: boolean;
  cuisine: CuisineType;
  imageUrl: string;
  address: Address;
}

export interface RestaurantDto {
  restaurantId: number;
  name: string;
  description: string;
  cuisineTypeId: number;
  isActive: boolean;
  isCurrentlyOpen: boolean;
  imageUrl: string;
  address: AddressDto;
  cuisine?: CuisineType;
  operatingHours: RestaurantHourDto[];
  menuItems?: MenuItemDto[];
}

export interface RestaurantCreateDto {
  name: string;
  description: string;
  address: AddressDto;
  imageUrl?: string;
  cuisineTypeId: number | null;
  searchTags: string[];
}

