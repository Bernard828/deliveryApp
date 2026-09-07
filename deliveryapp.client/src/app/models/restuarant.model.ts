import { Address, AddressDto } from "./address.model";
import { CuisineType } from "./cuisine-type.model";
import { MenuItemDto } from "./menuItem.model";
import { RestaurantHoursDto } from "./restaurant-hours.model";

export interface Restaurant {
  restaurantId: number;
  name: string;
  description?: string;
  imageUrl?: string;
  address?: Address;
  isActive: boolean;
  isCurrentlyOpen: boolean;
  operatingHours: RestaurantHoursDto[];

  cuisineTypeId: number;
  cuisine: CuisineType;
}

export interface RestaurantDto {
  restaurantId: number;
  name: string;
  description: string;
  isActive: boolean;

  cuisineTypeId: number | null;
  cuisineType?: CuisineType;
  cuisineTypeName?: string | null;

  imageUrl: string;

  isCurrentlyOpen: boolean;
  operatingHours: RestaurantHoursDto[];

  address: AddressDto | null;

  searchTags: string[];

  menuItems?: MenuItemDto[];
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

