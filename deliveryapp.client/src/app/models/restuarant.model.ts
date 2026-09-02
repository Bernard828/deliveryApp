import { Address, AddressDto } from "./address.model";
import { CuisineType } from "./cuisine-type.model";
import { MenuItemDto } from "./menuItem.model";
import { OperatingHourDto } from "./restaurant-hours.model";

export interface Restaurant {
  restaurantId: number;
  name: string;
  description?:string;
  imageUrl?: string;
  address?: Address;
  isActive: boolean;
  isCurrentlyOpen: boolean;
  operatingHours: OperatingHourDto[];

  cuisineTypeId: number;
  cuisine: CuisineType;
}

export interface RestaurantDto {
  restaurantId: number;
  name: string;
  description: string;
  isActive: boolean;
  cuisineTypeId: number;
  cuisineType?: CuisineType;
  cuisineTypeName?: string;
  imageUrl: string;
  isCurrentlyOpen: boolean;
  operatingHours: OperatingHourDto[];
  address: AddressDto;
  menuItems?: MenuItemDto[];
  searchTags: string[];
}

export interface RestaurantCreateDto {
  name: string;
  description: string;
  addresId: number;
  address: AddressDto;
  imageUrl?: string;
  isActive: boolean;
  cuisineTypeId: number | null;
  searchTags: string[];
}

